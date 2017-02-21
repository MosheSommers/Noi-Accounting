using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using NOIAcountingVersion2.ModelClasses;


namespace NOIAcountingVersion2.DalClasses
{
    public class TransactionSqlDAL
    {
        private const string sql_insertTransaction = "insert into transaction values(@transactionType, @transactionName, @amount, @date, @companyId)";
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
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql_insertTransaction, connection);
                    command.Parameters.AddWithValue("@transactionType", t.IsRevenue);
                    command.Parameters.AddWithValue("@transactionName", t.Description);
                    command.Parameters.AddWithValue("@amount", t.Amount);
                    command.Parameters.AddWithValue("@date", t.Date);
                    command.Parameters.AddWithValue("@companyId", t.CompanyId);

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

        public List<Transaction> GetAllTransactios()
        {
            return new List<Transaction>();
        }

        public List<Transaction> GetAllExpenses()
        {
            return new List<Transaction>();
        }

        public List<Transaction> GetAllRevenue()
        {
            return new List<Transaction>();
        }

        public List<Transaction> GetExpensesForTimePeriod()
        {
            return new List<Transaction>();
        }

        public List<Transaction> GetRevenueForTimePeriod()
        {
            return new List<Transaction>();
        }

    }
}
