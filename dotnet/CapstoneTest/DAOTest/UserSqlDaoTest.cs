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
            // Arrange - new instance of UserSqlDao before each and every test
            userSqlDao = new UserSqlDao(ConnectionString);
            base.Setup();
        }

        [TestMethod]
        public void GetUsers()
        {

            // Arrange  - see above


            // Act - retrieve users
            IList<User> users = userSqlDao.GetUsers();

            // Assert - retrieved list not null and matches expected count
            Assert.IsNotNull(users);
            Assert.AreEqual(2, users.Count);
        }

    }
}
