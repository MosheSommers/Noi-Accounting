using Microsoft.VisualStudio.TestTools.UnitTesting;
using NOI_AcountingVersion2.DalClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Data.SqlClient;
using NOI_AcountingVersion2.Model_Classes;

namespace NOI_AcountingVersion2.DalClasses.Tests
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
            }
        }

        [TestCleanup]
        public void CleanUp()
        {
            tran.Dispose();
        }

        [TestMethod()]
        [ExpectedException(typeof(SqlException))]
        public void CreateNewCompanyTest()
        {
            CompanySqlDAL companyDal = new CompanySqlDAL(connectionString);
            Company c = new Company()
            {
                CompanyId = 7,
                Name = "Hey"
            };

            bool addedComp = companyDal.CreateNewCompany(c);
            Assert.IsTrue(addedComp);
        }
    }
}