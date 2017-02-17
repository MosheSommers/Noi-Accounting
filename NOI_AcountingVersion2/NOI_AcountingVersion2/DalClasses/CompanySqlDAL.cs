using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using NOIAcountingVersion2.ModelClasses;

namespace NOIAcountingVersion2.DalClasses
{
    public class CompanySqlDAL
    {
        private string connectionString;
        private const string sql_insertCompany = "insert into company values(@companyId, @name)";
        private const string sql_updateCompanyName = "update company set name = @name where company_id = @companyId";

        public CompanySqlDAL(string databaseConnectionString)
        {
            connectionString = databaseConnectionString;
        }

        //Method to create new company
        public bool CreateNewCompany(Company c)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql_insertCompany, connection);
                    command.Parameters.AddWithValue("@companyId", c.CompanyId);
                    command.Parameters.AddWithValue("@name", c.Name);
                    int rowsAffected = command.ExecuteNonQuery();

                    //returns true if one row was affected
                    return(rowsAffected == 1);
                }
            }

            //Find out about how to handle exceptions
            catch (SqlException e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Sorry can't connect to the database please try again later.");
                throw;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Sorry something went wrong please try again later.");
                throw;
            }
        }

        //Method to change your company's name takes in a company object 
        //that consists of the company id and the new name

        public bool ChangeCompanyName(Company c)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql_updateCompanyName, connection);
                    command.Parameters.AddWithValue("@companyId", c.CompanyId);
                    command.Parameters.AddWithValue("@name", c.Name);

                    int rowsAffected = command.ExecuteNonQuery();

                    return (rowsAffected == 1);
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Sorry can't connect to the database please try again later.");
                throw;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Sorry something went wrong please try again later.");
                throw;
            }
        }
    }
}
