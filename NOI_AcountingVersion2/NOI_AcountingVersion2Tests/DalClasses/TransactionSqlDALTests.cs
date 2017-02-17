using Microsoft.VisualStudio.TestTools.UnitTesting;
using NOIAcountingVersion2.DalClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Transactions;

namespace NOIAcountingVersion2.DalClasses.Tests
{
    [TestClass()]
    public class TransactionSqlDALTests
    {
        TransactionScope tran;
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=NOI Accounting;" +
            "User ID=te_student;Password=sqlserver1";

        [TestMethod]
        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();
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
            Assert.Fail();
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
    }
}