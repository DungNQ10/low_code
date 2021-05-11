using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Lowcode.Domain.Data;
using Lowcode.Domain.Data.Entity;
using Lowcode.Domain.Services.AttachmentsFacade;
using Lowcode.Domain.Services.RolesFacade;
using Lowcode.Framework.Data;
using Lowcode.Framework.Utils;

namespace Lowcode.Domain.Services.UserFacade.Implementation
{
    public class UserService : IUserService
    {
        private IEntityRepository<User, DomainContext> _userRepository;
        private IAttachmentsService attachmentsService;

        public UserService(IEntityRepository<User, DomainContext> userRepository,
            IAttachmentsService attachmentsService)
        {
            this.attachmentsService = attachmentsService;
            _userRepository = userRepository;
        }

        public ActionResultType<UserModel> Add(UserModel user)
        {
            if (String.IsNullOrEmpty(user.Password))
            {
                return new ActionResultType<UserModel>()
                {
                    Success = false,
                    Message = "Mật khẩu là bắt buộc"
                };
            }

            user.Status = UserStatus.Actived;
            var exist = _userRepository.Single(c => c.Email == user.Email);
            if (exist != null)
            {
                return new ActionResultType<UserModel>()
                {
                    Success = false,
                    Message = "Email đã tồn tại"
                };
            }

            //add to storage
            user.Password = EncryptUtil.EncryptMD5(user.Password);
            user.CreatedDate = DateTime.Now;
            user.UserModifiedTimeStamp = Guid.NewGuid();

            var entity = user.Map<User>();
            var res = _userRepository.Add(entity);
            var model = res != null ? res.Map<UserModel>() : null;

            return new ActionResultType<UserModel>()
            {
                Success = (res != null ? true : false),
                Data = model
            };
        }

        public async Task<ActionResult> ChangePassword(int userId, string oldPassword, string newPassword)
        {
            //validate
            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
                return new ActionResult()
                {
                    Success = false,
                    Message = "User không tồn tại"
                };

            if (user.Password != EncryptUtil.EncryptMD5(oldPassword))
            {
                return new ActionResult()
                {
                    Success = false,
                    Message = "Mật khẩu cũ không đúng"
                };
            }

            //update old password to storage and not change
            user.Password = EncryptUtil.EncryptMD5(newPassword);
            user.UpdatedDate = DateTime.Now;
            user.UserModifiedTimeStamp = Guid.NewGuid();
            var res = await _userRepository.UpdateAsync(user);

            return new ActionResult()
            {
                Success = res
            };
        }

        public async Task<ActionResult> Delete(int[] Ids)
        {
            var res = await _userRepository.DeleteManyAsync(c => Ids.Contains(c.Id));

            return new ActionResult()
            {
                Success = (res > 0 ? true : false)
            };
        }

        public async Task<ActionResult> DeleteManyAsync(Expression<Func<User, bool>> expression)
        {
            var res = await _userRepository.DeleteManyAsync(expression);

            return new ActionResult()
            {
                Success = (res > 0 ? true : false)
            };
        }

        public async Task<UserModel> GetByIdAsync(int userId)
        {
            var entity = await _userRepository.GetByIdAsync(userId);
            if (entity != null)
                entity.Password = null;
            return entity.Map<UserModel>();
        }

        public UserModel GetById(int userId)
        {
            var entity = _userRepository.GetById(userId);
            if (entity != null)
                entity.Password = null;

            var userModel = entity.CloneToModel<User, UserModel>();
            var roleIds = new List<int>();
            if (!string.IsNullOrWhiteSpace(userModel.UserRoles))
            {
                roleIds = userModel.UserRoles.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries)
                      .Select(c => Convert.ToInt32(c)).ToList();
            }

            var context = _userRepository.GetDbContext();

            var roles = context.Roles.Where(c => roleIds.Contains(c.Id)).ToList();
            if (roles.Count > 0)
            {
                userModel.RoleNames = string.Join(", ", roles.Select(c => c.RoleName).ToList());
            }

            return userModel;
        }

        public async Task<List<UserModel>> List(UserFilterModel filter)
        {
            if (filter == null)
                return null;

            using (var db = _userRepository.GetDbContext())
            {
                var q = db.Users.AsQueryable();

                if (filter.AccountType != null)
                {
                    var accountType = (int)filter.AccountType;
                    q = q.Where(c => c.AccountType == (AccountType)accountType);
                }

                if (!String.IsNullOrEmpty(filter.Search))
                {
                    q = q.Where(c => c.Address.Contains(filter.Search) || c.Email.Contains(filter.Search) || c.FullName.Contains(filter.Search) || c.Mobile.Contains(filter.Search));
                }

                if (filter.CreatorId != null)
                {
                    q = q.Where(c => c.CreatedBy == filter.CreatorId);
                }

                if (filter.UpdaterId != null)
                {
                    q = q.Where(c => c.UpdatedBy == filter.UpdaterId);
                }

                if (filter.UpdaterId != null)
                {
                    q = q.Where(c => c.UpdatedBy == filter.UpdaterId);
                }

                if (filter.UserStatus != null)
                {
                    q = q.Where(c => c.Status == filter.UserStatus);
                }

                if (!String.IsNullOrEmpty(filter.Search))
                {
                    q = q.Where(c => c.FullName.Contains(filter.Search) || c.Address.Contains(filter.Search) || c.Email.Contains(filter.Search) || c.Mobile.Contains(filter.Search));
                }
                if (filter.SelectedRoleKeys.Count > 0)
                {
                    var predicate = PredicateBuilder.False<User>();
                    foreach (var item in filter.SelectedRoleKeys)
                    {
                        var key = ";" + item.ToString() + "";
                        predicate = predicate.Or(c => c.UserRoles.Contains(key));
                    }
                    q = q.Where(predicate);
                }

                filter.Paging.RowsCount = await q.CountAsync();

                var result = await q.OrderByDescending(c => c.CreatedDate).Skip(filter.Paging.StartRowIndex).Take(filter.Paging.PageSize)
                    .Select(c => new UserModel()
                    {
                        Id = c.Id,
                        FullName = c.FullName,
                        Birthday = c.Birthday,
                        Email = c.Email,
                        Gender = c.Gender,
                        Mobile = c.Mobile,
                        Status = (UserStatus)c.Status,
                        CreatedDate = c.CreatedDate,
                        UpdatedDate = c.UpdatedDate,
                        CreatedBy = c.CreatedBy,
                        UpdatedBy = c.UpdatedBy,
                        UserRoles = c.UserRoles,
                        Address = c.Address,
                        AccountType = (AccountType)c.AccountType,
                        ObjectId = c.ObjectId,
                        WhiteList = c.WhiteList,
                    }).ToListAsync();

                foreach (var item in result)
                {
                    if (!String.IsNullOrEmpty(item.UserRoles) && item.UserRoles.Contains(";"))
                    {
                        var ids = item.UserRoles.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries).Select(c => Convert.ToInt32(c));
                        var roles = db.Roles.Where(c => ids.Contains(c.Id)).ToList();
                        item.RoleNames = string.Join(", ", roles.Select(c => c.RoleName));
                    }
                }

                return result;
            }//using
        }

        public async Task<ActionResult> ResetPassword(int userId, string newPassword)
        {
            if (String.IsNullOrEmpty(newPassword))
                return new ActionResult()
                {
                    Success = false,
                    Message = "Mật khẩu không hợp lệ"
                };

            //validate
            var staff = await _userRepository.GetByIdAsync(userId);

            if (staff == null)
                return new ActionResult()
                {
                    Success = false,
                    Message = "User không tồn tại"
                };

            //update old password to storage and not change
            staff.Password = EncryptUtil.EncryptMD5(newPassword);
            staff.UpdatedDate = DateTime.Now;
            staff.UserModifiedTimeStamp = Guid.NewGuid();
            var res = await _userRepository.UpdateAsync(staff);

            return new ActionResult()
            {
                Success = res
            };
        }

        public async Task<UserModel> SingleAsync(Expression<Func<User, bool>> query)
        {
            var res = await _userRepository.SingleAsync(query);
            //if (res != null)
            //    res.Password = null;
            return res.Map<UserModel>();
        }

        public async Task<ActionResult> ToggleLock(int staffId)
        {
            var res = await _userRepository.GetByIdAsync(staffId);
            if (res != null)
            {
                res.Status = res.Status == UserStatus.Actived ? UserStatus.Disabled : UserStatus.Actived;
                await _userRepository.UpdateAsync(res);
                return new ActionResult() { Success = true };
            }
            return new ActionResult() { Success = false, Message = "User không tồn tại" };
        }

        public ActionResult Update(UserModel staff)
        {
            return Update(staff, true);
        }

        public ActionResult Update(UserModel user, bool forceChangeTimestamp)
        {
            var exist = _userRepository.GetById(user.Id);
            var context = _userRepository.GetDbContext();
            if (exist == null)
                return new ActionResult()
                {
                    Success = false,
                    Message = "user không tồn tại"
                };

            exist = _userRepository.Single(c => c.Email == user.Email && c.Id != user.Id);
            if (exist != null)
            {
                return new ActionResult()
                {
                    Success = false,
                    Message = "Email đã tồn tại"
                };
            }
            exist = _userRepository.GetById(user.Id);

            //update old password to storage and not change
            user.Password = exist.Password;
            user.AccountType = exist.AccountType;
            user.ObjectId = exist.ObjectId;

            user.UpdatedDate = DateTime.Now;

            if (forceChangeTimestamp == true)
                user.UserModifiedTimeStamp = Guid.NewGuid();

            //update to storage
            var entity = user.Map<User>();
            if (!entity.WorkingPackageId.HasValue)
            {
            }
            var res = _userRepository.Update(entity);
            var model = entity.Map<UserModel>();
            if (user.Attachments != null)
            {
                foreach (var at in user.Attachments)
                {
                    at.ItemId = user.Id;
                }
                attachmentsService.SaveList(user.Attachments);
            }

            return new ActionResult()
            {
                Success = res,
                Data = model
            };
        }

        public ActionResultType<UserModel> VerifyPassword(string userName, string encryptedPassword)
        {
            var user = _userRepository.Single(u => u.Email == userName || u.Mobile == userName);
            if (user == null)
                return new ActionResultType<UserModel>()
                {
                    Success = false,
                    Message = "User is not exist"
                };

            if (user.Status != UserStatus.Actived)
                return new ActionResultType<UserModel>()
                {
                    Success = false,
                    Message = "User is not activated"
                };

            if (user.Password == encryptedPassword)
            {
                var userModel = user.Map<UserModel>();
                return new ActionResultType<UserModel>()
                {
                    Success = true,
                    Data = userModel
                };
            }

            return new ActionResultType<UserModel>()
            {
                Success = false,
                Message = "Password is not valid"
            };
        }

        public async Task<ActionResult> ChangePassword(ChangePasswordModel changePassword)
        {
            return await ChangePassword(changePassword.UserId, changePassword.OldPassword, changePassword.NewPassword);
        }

        public async Task<List<UserModel>> ListAll(UserStatus? status)
        {
            var users = _userRepository.Fetch(c => c.Status == status || status == null).ToList();
            var userModels = users.Map<List<UserModel>>();
            userModels.ForEach(c =>
            {
                c.Password = null;
            });
            return userModels;
        }

        public List<int> GetRoles(int userId)
        {
            using (var context = _userRepository.GetDbContext())
            {
                var user = context.Users.FirstOrDefault(c => c.Id == userId);
                if (user != null)
                {
                    var roleIds = user.UserRoles.ParseIds();
                    if (roleIds != null)
                        return roleIds.ToList();
                }
                return new List<int>();
            }
        }

        public bool CheckModified(string timeStemp, int userId)
        {
            using (var context = _userRepository.GetDbContext())
            {
                var usertimestemp = context.Users.Where(c => c.Id == userId).Select(c => c.UserModifiedTimeStamp).FirstOrDefault();
                if (usertimestemp == null)
                    return true;
                if (usertimestemp.ToString() == timeStemp)
                    return false;
                else
                    return true;
            }
        }

        public void UpdateUserTimeStemp(List<int> roles)
        {
            using (var context = _userRepository.GetDbContext())
            {
                if (roles.Count > 0)
                {
                    var p = PredicateBuilder.False<User>();

                    foreach (var role in roles)
                    {
                        var roleString = ";" + role + ";";
                        p = p.Or(c => c.UserRoles.Contains(roleString));
                    }

                    var users = context.Users.Where(p).Distinct().ToList();
                    foreach (var item in users)
                    {
                        item.UserModifiedTimeStamp = Guid.NewGuid();
                    }
                    context.SaveChanges();
                }
            }
        }

        public ActionResult SaveIPs(UserFormIPModel model)
        {
            using (var context = _userRepository.GetDbContext())
            {
                var users = context.Users.Where(c => model.UserIds.Contains(c.Id)).ToList();

                foreach (var item in users)
                {
                    item.WhiteList = model.IpAddress;
                }
                context.SaveChanges();
                return new ActionResult()
                {
                    Success = true,
                    Data = true
                };
            }
        }

        public List<UserModel> GetUserByPermission(PermissionCode permission)
        {
            using (var  context = _userRepository.GetDbContext())
            {
                var users = context.Users.Where(c =>
                    c.UserRoles.Contains($";{permission.GetHashCode().ToString()};")).ToList();

                var model = users.CloneToListModels<User, UserModel>();
                return model;
            }
        }

        public List<UserModel> GetUserByPermission(List<PermissionCode> permissions )
        {
            using (var context = _userRepository.GetDbContext())
            {
                var res = new List<UserModel>();
                foreach (var item in permissions)
                {
                    var users = context.Users.Where(c =>
                        c.UserRoles.Contains($";{item.GetHashCode().ToString()};")).ToList();

                    var model = users.CloneToListModels<User, UserModel>();
                    if(model.Count>0)
                        res.AddRange(model);
                }
              
                return res.Distinct().ToList();
            }
        }

    }
}