using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NOIAcountingVersion2.ModelClasses;
using System.Data.SqlClient;

namespace NOIAcountingVersion2.DalClasses
{
    public class UserSqlDal : IUserDal
    {
        private const string UpdateUserNameQuery = "update user_info set name = @name where company_id = @companyId";
        private string connectionString;
        public UserSqlDal(String databaseConnectionString)
        {
            connectionString = databaseConnectionString;
        }
        public bool ChangeUserName(User u)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(UpdateUserNameQuery, connection);
                    command.Parameters.AddWithValue("@companyId", u.CompanyId);
                    command.Parameters.AddWithValue("@name", u.Name);

                    int rowsAffected = command.ExecuteNonQuery();

                    return (rowsAffected == 1);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool CreateNewUser(User u)
        {
            throw new NotImplementedException();
        }
    }
}
