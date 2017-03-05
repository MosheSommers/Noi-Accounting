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
        private const string insertTransactionQuery = "insert into transaction values(@transactionType, @transactionName, @amount, @date, @companyId, @userId)";
        private const string allTransactionsQuery = "select * from transaction where company_id = @companyId";
        private const string expensesQuery = "select from transaction where comapany_id = @companyId and transaction_type = false";
        private const string revenueQuery = "select from transaction where comapany_id = @companyId and transaction_type = true";
        private string connectionString;
        public TransactionSqlDAL(string databaseConnectionString)
        {
            this.connectionString = databaseConnectionString;
        }

        //Stores transaction in database
        public bool CreateTransaction(Transaction t)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(insertTransactionQuery, connection);
                    command.Parameters.AddWithValue("@transactionType", t.IsRevenue);
                    command.Parameters.AddWithValue("@transactionName", t.Description);
                    command.Parameters.AddWithValue("@amount", t.Amount);
                    command.Parameters.AddWithValue("@date", t.Date);
                    command.Parameters.AddWithValue("@companyId", t.CompanyId);
                    command.Parameters.AddWithValue("@userId", t.UserId);

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
               return GetTransactions(u, allTransactionsQuery);
        }
       
        public List<Transaction> GetCompanyExpenses(User u)
        {
            return GetTransactions(u, expensesQuery);
        }

        public List<Transaction> GetCompanyRevenue(User u)
        {
            return GetTransactions(u, revenueQuery);
        }

        public List<Transaction> GetExpensesForTimePeriod(User u)
        {
            return new List<Transaction>();
        }

        public List<Transaction> GetRevenueForTimePeriod(User u)
        {
            return new List<Transaction>();
        }


        //Helper Method to query transactions from database and
        private List<Transaction> GetTransactions(User u, string query)
        {
            try
            {
                List<Transaction> transactions = new List<Transaction>();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@companyId", u.CompanyId);

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

    }
}
