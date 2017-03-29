using Microsoft.VisualStudio.TestTools.UnitTesting;
using NOIAcountingVersion2.DalClasses;
using NOIAcountingVersion2.ModelClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Data.SqlClient;

namespace NOIAcountingVersion2.DalClasses.Tests
{
    [TestClass()]
    public class UserSqlDalTests
    {
        private TransactionScope tran;
        string companyPassword;

        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=NOI Accounting;" +
            "User ID=te_student;Password=sqlserver1";

        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("insert into user_info values('temporary user', '12345678', null)", connection);
                command.ExecuteNonQuery();

            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod()]
        public void ChangeUserNameTest()
        {
            UserSqlDal userDal = new UserSqlDal(connectionString);
            User u = new User()
            {
                Password = "12345678",
                Name = "ChangedName"
            };
            bool changedname = userDal.ChangeUserName(u);
            Assert.IsTrue(changedname);

        }

        [TestMethod()]
        public void ChangeUserPasswordTest()
        {
            UserSqlDal userDal = new UserSqlDal(connectionString);
            User u = new User()
            {
                Password = "12345678"
            };

            bool changedPassword = userDal.ChangeUserPassword(u, "11111111");
            Assert.IsTrue(changedPassword);
        }

        [TestMethod()]
        public void CreateNewUserTest()
        {
            UserSqlDal userDal = new UserSqlDal(connectionString);

            User u = new User()
            {
                Name = "NewUser",
                Password = "87654321",

            };
            Company c = new Company
            {
                Name = "temporaryComp",
                Password = "companys"
            };

            CreateCompany();

            bool createdUser = userDal.CreateNewUser(u, c);
            Assert.IsTrue(createdUser);

        }

        static string CreateCompany()
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=NOI Accounting;User ID=te_student;Password=sqlserver1"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("insert into company values('temporaryComp', 'companys')", connection);

                command.ExecuteNonQuery();
                return "companys";
            }
        }
    }
}