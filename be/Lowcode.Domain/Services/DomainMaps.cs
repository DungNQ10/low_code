using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Lowcode.Domain.Data.Entity;
using Lowcode.Domain.Services.AttachmentsFacade;
using Lowcode.Domain.Services.RolesFacade;
using Lowcode.Domain.Services.UserFacade;

namespace Lowcode.Domain.Services
{
    public static class DomainMaps
    {
        public static IMapper Mapper { get; set; }

        public static void Config()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddCollectionMappers();
                cfg.CreateMap<User, UserModel>();
                cfg.CreateMap<Roles, RolesModel>();
                cfg.CreateMap<Attachments, AttachmentsModel>();

                // ignore all unmapped properties globally
                //cfg.ForAllMaps((map, exp) => exp.ForAllOtherMembers(opt => opt.Ignore()));
            });
            Mapper = config.CreateMapper();
        }

        public static TTo Map<TTo>(this object entity)
        {
            return Mapper.Map<TTo>(entity);
        }
    }
}