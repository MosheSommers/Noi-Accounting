using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NOIAcountingVersion2.ModelClasses;


namespace NOIAcountingVersion2.DalClasses
{
    public class TransactionSqlDAL
    {
        private string connectionString;
        public TransactionSqlDAL(string databaseConnectionString)
        {
            this.connectionString = databaseConnectionString;
        }

        //Stores transaction in database
        public bool CreateTransaction()
        {
            return false;
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
