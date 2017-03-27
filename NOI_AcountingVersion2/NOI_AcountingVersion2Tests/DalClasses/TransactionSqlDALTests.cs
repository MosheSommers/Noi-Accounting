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
        Company c; 
        ModelClasses.Transaction t;


       
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=NOI Accounting;" +
            "User ID=te_student;Password=sqlserver1";


        [TestMethod]
        [TestInitialize]
        public void Initialize()
        {
         
            tran = new TransactionScope();

            c = CreateCompany();
            t = CreateTestTransaction();
        }

        

        [TestMethod]
        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod()]
        public void CreateTransactionTest()
        {
            TransactionSqlDAL dal = new TransactionSqlDAL(connectionString);

            bool created = dal.CreateTransaction(t);
            Assert.IsTrue(created);
        }

        [TestMethod()]
        public void GetAllTransactiosTest()
        {
            Assert.Fail();
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
        private User CreateUser(Company c)
        {
            User u = new User()
            {
                Name = "testUser",
                Password = "testUserPassword",
                CompanyId = c.CompanyId
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
                CompanyId = 1234567
            };

            return t;
        }
    }
}