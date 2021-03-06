﻿using System;
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
        private const string UpdateUserNameQuery = "update user_info set user_name = @newName where user_name = @name and password = @password";
        private const string UpdateUserPasswordQuery = "update user_info set password = @newPassword where user_name = @name and password = @password";
        private const string InsertUserQuery = "insert into user_info values(@username, @password, (select company_id from company where company.name = @companyName))";

        private string connectionString;
        public UserSqlDal(String databaseConnectionString)
        {
            connectionString = databaseConnectionString;
        }

        public bool ChangeUserName(User u, string newName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(UpdateUserNameQuery, connection);
                    command.Parameters.AddWithValue("@password", u.Password);
                    command.Parameters.AddWithValue("@name", u.Name);
                    command.Parameters.AddWithValue("@newName", newName);

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
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Sorry something went wrong please try again later.");
                throw;
            }
        }

        public bool ChangeUserPassword(User u, string newPassword)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(UpdateUserPasswordQuery, connection);
                    command.Parameters.AddWithValue("@password", u.Password);
                    command.Parameters.AddWithValue("@name", u.Name);
                    command.Parameters.AddWithValue("@newPassword", newPassword);

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
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Sorry something went wrong please try again later.");
                throw;
            }
        }

        public bool CreateNewUser(User u, Company c)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(InsertUserQuery, connection);

                    command.Parameters.AddWithValue("@userName", u.Name);
                    command.Parameters.AddWithValue("@password", u.Password);
                    command.Parameters.AddWithValue("@companyPassword", c.Password);
                    command.Parameters.AddWithValue("@companyName", c.Name);

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
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Sorry something went wrong please try again later.");
                throw;
            }
        }
    }
}
