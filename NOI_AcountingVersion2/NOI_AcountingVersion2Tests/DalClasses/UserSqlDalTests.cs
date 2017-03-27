using Microsoft.VisualStudio.TestTools.UnitTesting;
using NOIAcountingVersion2.DalClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOIAcountingVersion2.DalClasses.Tests
{
    [TestClass()]
    public class UserSqlDalTests
    {
        private TransactionScope tran;
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=NOI Accounting;" +
            "User ID=te_student;Password=sqlserver1";

        [TestMethod()]
        public void UserSqlDalTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ChangeUserNameTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CreateNewUserTest()
        {
            Assert.Fail();
        }
    }
}