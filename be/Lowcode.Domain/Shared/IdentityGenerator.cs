using System.Data.SqlClient;
using Lowcode.Domain.Data;

namespace Lowcode.Domain.Shared
{
    /// <summary>
    /// Gen id from db
    /// </summary>
    public class IdentityGenerator
    {
        /// <summary>
        /// Auto generate from identity
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="ColumnName"></param>
        /// <param name="ColumnLength"></param>
        /// <param name="Prefix"></param>
        /// <param name="Padding"></param>
        /// <returns></returns>
        public static string GenerateIdentity(string TableName, string ColumnName, int ColumnLength, string Prefix, bool Padding)
        {
            string query, Id;

            //using (var data = new DomainContext())
            using (SqlConnection cn = new SqlConnection(DomainContext.ConnectionString))
            {
                int preLength, padLength;
                preLength = Prefix.Length;
                padLength = ColumnLength - preLength;

                query = "SELECT MAX(" + ColumnName + ") + 1 FROM " + TableName;

                using (SqlCommand com = new SqlCommand(query, cn))
                {
                    cn.Open();
                    var res = com.ExecuteScalar();
                    var padRemain = padLength - res.ToString().Length;

                    Id = Prefix;
                    if (Padding == true)
                    {
                        for (int i = 0; i < padRemain; i++)
                        {
                            Id += "0";
                        }
                    }
                    Id += res;
                    return Id;
                }
            }//using
        }
    }
}