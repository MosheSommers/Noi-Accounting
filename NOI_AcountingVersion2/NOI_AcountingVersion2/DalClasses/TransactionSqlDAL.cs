using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using NOIAcountingVersion2.ModelClasses;


namespace NOIAcountingVersion2.DalClasses
{
    public class TransactionSqlDAL : ITransactionDAL
    {
        //SQL queries
        private const string InsertTransactionQuery = "insert into my_transaction values(@transactionType, @transactionName, @amount," +
            " @date, (select company_id from user_info where user_name = @userName and password = @userPassword)," +
            " (select user_id from user_info where user_name = @userName and password = @userPassword))";

        private const string AllTransactionsQuery = "select * from my_transaction where company_id = (select user_info.company_id from user_info where user_name = @userName and password = @userPassword)";
        private const string ExpensesQuery = "select * from my_transaction where comapany_id = @companyId and transaction_type = false";
        private const string RevenueQuery = "select * from my_transaction where comapany_id = @companyId and transaction_type = true";
        private const string ExepensesForTimePeriodQuery = "select * from my_transaction where date >= @start and date <= @end and transaction_type = false and company_id = @companyId";
        private const string RevenueForTimePeriodQuery = "select * from my_transaction where date >= @start and date <= @end and transaction_type = true and company_id = @companyId";

        private string connectionString;
        public TransactionSqlDAL(string databaseConnectionString)
        {
            this.connectionString = databaseConnectionString;
        }

        //Stores transaction in database
        public bool CreateTransaction(Transaction t, User u)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(InsertTransactionQuery, connection);
                    command.Parameters.AddWithValue("@transactionType", t.IsRevenue);
                    command.Parameters.AddWithValue("@transactionName", t.Description);
                    command.Parameters.AddWithValue("@amount", t.Amount);
                    command.Parameters.AddWithValue("@date", t.Date);
                    command.Parameters.AddWithValue("@userName", u.Name);
                    command.Parameters.AddWithValue("@userPassword", u.Password);

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

        public List<Transaction> GetCompanyTransactions(User u)
        {
            return GetTransactions(u, AllTransactionsQuery);
        }

        public List<Transaction> GetCompanyExpenses(User u)
        {
            return GetTransactions(u, ExpensesQuery);
        }

        public List<Transaction> GetCompanyRevenue(User u)
        {
            return GetTransactions(u, RevenueQuery);
        }

        public List<Transaction> GetExpensesForTimePeriod(User u, TimePeriod t)
        {
            return GetTransactionsForTimePeriod(u, t, ExepensesForTimePeriodQuery);
        }
        
        public List<Transaction> GetRevenueForTimePeriod(User u, TimePeriod t)
        {
            return GetTransactionsForTimePeriod(u, t, RevenueForTimePeriodQuery);
        }


        //Helper Method to query transactions from database and return list of transaction
        private List<Transaction> GetTransactions(User u, string query)
        {
            try
            {
                List<Transaction> transactions = new List<Transaction>();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@userName", u.Name);
                    command.Parameters.AddWithValue("@userPassword", u.Password);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Transaction t = new Transaction();
                        t.TransactionId = Convert.ToInt32(reader["transaction_id"]);
                        t.IsRevenue = Convert.ToBoolean(reader["transaction_type"]);
                        t.Description = Convert.ToString(reader["transaction_name"]);
                        t.Amount = Convert.ToDouble(reader["amount"]);
                        t.Date = Convert.ToDateTime(reader["date"]);
                        t.CompanyId = Convert.ToInt32(reader["company_id"]);
                        t.UserId = Convert.ToInt32(reader["user_id"]);

                        transactions.Add(t);
                    }
                }
                return transactions;
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

        //Helper method to get transactions for a specific time period
        private List<Transaction> GetTransactionsForTimePeriod(User u, TimePeriod t, string query)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@start", t.Start);
                    command.Parameters.AddWithValue("@end", t.End);
                    command.Parameters.AddWithValue("@companyId", u.CompanyId);

                    SqlDataReader reader = command.ExecuteReader();

                    List<Transaction> transactions = new List<Transaction>();

                    while (reader.Read())
                    {
                        Transaction tran = new Transaction();
                        tran.TransactionId = Convert.ToInt32(reader["transaction_id"]);
                        tran.IsRevenue = Convert.ToBoolean(reader["transaction_type"]);
                        tran.Description = Convert.ToString(reader["transaction_name"]);
                        tran.Amount = Convert.ToDouble(reader["amount"]);
                        tran.Date = Convert.ToDateTime(reader["date"]);
                        tran.CompanyId = Convert.ToInt32(reader["company_id"]);
                        tran.UserId = Convert.ToInt32(reader["user_id"]);

                        transactions.Add(tran);
                    }
                    return transactions;
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
