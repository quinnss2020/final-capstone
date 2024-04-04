using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Capstone.DAO;
using Capstone.Models;

namespace Tutorial.Tests.DAO
{
    [TestClass]
    public class UserSqlDaoTest : BaseDaoTests
    {

        private UserSqlDao userSqlDao;

        [TestInitialize]
        public override void Setup()
        {
            // Arrange - new instance of SaleSqlDao before each and every test
            userSqlDao = new UserSqlDao(ConnectionString);
            base.Setup();
        }

        [TestMethod]
        public void GetPets()
        {

            // Arrange 


            // Act - retrieve Madge's first sale
            IList<User> users = userSqlDao.GetUsers();

            // Assert - retrieved sale is not null and matches expected sale
            Assert.IsNotNull(users);
            Assert.AreEqual(2, users.Count);
        }

    }
}
