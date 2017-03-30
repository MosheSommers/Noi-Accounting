using Microsoft.VisualStudio.TestTools.UnitTesting;
using NOIAcountingVersion2.DalClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Data.SqlClient;
using NOIAcountingVersion2.ModelClasses;

namespace NOIAcountingVersion2.DalClasses.Tests
{
    [TestClass()]
    public class CompanySqlDALTests
    {
        private TransactionScope tran;
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=NOI Accounting;" +
            "User ID=te_student;Password=sqlserver1";

        [TestInitialize]
        public void Initiialize()
        {
            tran = new TransactionScope();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("insert into company values('Temporary Company', '12345678')", connection);
                command.ExecuteNonQuery();
            }
        }

        [TestCleanup]
        public void CleanUp()
        {
            tran.Dispose();
        }

        [TestMethod()]
        public void CreateNewCompanyTest()
        {
            CompanySqlDAL companyDal = new CompanySqlDAL(connectionString);
            Company c = new Company() { Password = "87654321", Name = "Hey" };

            bool addedComp = companyDal.CreateNewCompany(c);
            Assert.IsTrue(addedComp);
        }

        [TestMethod]
        public void ChangeCompanyNameTest()
        {
            CompanySqlDAL companyDal = new CompanySqlDAL(connectionString);

            Company c = new Company() { Password = "12345678", Name = "Temporary Company" };
            bool NameChanged = companyDal.ChangeCompanyName(c, "Permanent Comp");

            Assert.IsTrue(NameChanged);
        }

        [TestMethod]
        public void ChangeCompanyPasswordTest()
        {
            CompanySqlDAL companyDal = new CompanySqlDAL(connectionString);

            Company c = new Company() { Name = "Temporary Company", Password = "12345678" };
            bool NameChanged = companyDal.ChangeCompanyPassword(c, "11111111");

            Assert.IsTrue(NameChanged);
        }
    }
}