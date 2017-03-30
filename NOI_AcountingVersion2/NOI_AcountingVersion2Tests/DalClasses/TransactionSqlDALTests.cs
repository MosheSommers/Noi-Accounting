using Microsoft.VisualStudio.TestTools.UnitTesting;
using NOIAcountingVersion2.DalClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using NOIAcountingVersion2.ModelClasses;
using System.Transactions;

namespace NOIAcountingVersion2.DalClasses.Tests
{
    [TestClass()]
    public class TransactionSqlDALTests
    {
        TransactionScope tran;
        //Company c;
        User u;
        ModelClasses.Transaction t;


       
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=NOI Accounting;" +
            "User ID=te_student;Password=sqlserver1";
       
        [TestInitialize]
        public void Initialize()
        {       
            tran = new TransactionScope();

            
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("insert into company values('testCompany', 'companyP')", connection);
                command.ExecuteNonQuery();
                command = new SqlCommand("insert into user_info values('testUser', 'testUPas', (select company_id from company where company.password = 'companyP' and company.name = 'testCompany'))", connection);
                command.ExecuteNonQuery();
                command = new SqlCommand("insert into my_transaction values(1, 'rent', 900, '10/12/2012', (select company_id from user_info where user_name = 'testUser' and password = 'testUPas'), (select user_id from user_info where user_name = 'testUser' and password = 'testUPas'))", connection);
                command.ExecuteNonQuery();
            }

            u = CreateUser();
            t = CreateTestTransaction();
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod()]
        public void CreateTransactionTest()
        {
            TransactionSqlDAL dal = new TransactionSqlDAL(connectionString);

            bool created = dal.CreateTransaction(t, u);
            Assert.IsTrue(created);
        }

        [TestMethod()]
        public void GetAllTransactiosTest()
        {
            TransactionSqlDAL dal = new TransactionSqlDAL(connectionString);

            List<ModelClasses.Transaction> trans = dal.GetCompanyTransactions(u);

            Assert.IsTrue(trans.Count > 0);
        }

        [TestMethod()]
        public void GetAllExpensesTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetAllRevenueTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetExpensesForTimePeriodTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetRevenueForTimePeriodTest()
        {
            Assert.Fail();
        }
        //Helper Method to create company
        private Company CreateCompany()
        {
            Company c = new Company()
            {
                Name = "testCompany",
                Password = "testPassword"               
            };

            return c;
        }

        //Helper Method to create user
        private User CreateUser()
        {
            User u = new User()
            {
                Name = "testUser",
                Password = "testUPas",
            };

            return u;
        }
        //HelperMethod to create transaction
        private ModelClasses.Transaction CreateTestTransaction()
        {
            ModelClasses.Transaction t = new ModelClasses.Transaction()
            {
                IsRevenue = false,
                Description = "Rent",
                Amount = 1005.24,
                Date = DateTime.Now,
            };

            return t;
        }
    }
}