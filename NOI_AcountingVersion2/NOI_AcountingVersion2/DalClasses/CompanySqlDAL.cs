﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using NOIAcountingVersion2.ModelClasses;
using NOIAcountingVersion2.DalClasses;

namespace NOIAcountingVersion2.DalClasses
{
    public class CompanySqlDAL : ICompanyDAL
    {
        private string connectionString;
        private const string InsertCompanyQuery = "insert into company values(@name, @password)";
        private const string UpdateCompanyNameQuery = "update company set name = @newName where name = @name and password = @password";
        private const string UpdateCompanyPasswordQuery = "update company set password = @newPassword where name = @name and password = @password";

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
                    SqlCommand command = new SqlCommand(InsertCompanyQuery, connection);                   
                    command.Parameters.AddWithValue("@name", c.Name);
                    command.Parameters.AddWithValue("@password", c.Password);
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
        //that consists of the company id and name

        public bool ChangeCompanyName(Company c, string newName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(UpdateCompanyNameQuery, connection);
                    command.Parameters.AddWithValue("@password", c.Password);
                    command.Parameters.AddWithValue("@name", c.Name);
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
            catch(Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Sorry something went wrong please try again later.");
                throw;
            }
        }

        public bool ChangeCompanyPassword(Company c, string newPassword)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(UpdateCompanyPasswordQuery, connection);
                    command.Parameters.AddWithValue("@password", c.Password);
                    command.Parameters.AddWithValue("@name", c.Name);
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
    }
}
