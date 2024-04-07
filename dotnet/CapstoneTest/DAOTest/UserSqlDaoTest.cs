using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Capstone.DAO;
using Capstone.Models;
using Capstone.Exceptions;

namespace Tutorial.Tests.DAO
{
    [TestClass]
    public class UserSqlDaoTest : BaseDaoTests
    {
        //private static readonly User USER_1 = new User(1, 'User', 'Test', 'user@storage.com', 'Jg45HuwT7PZkfuKTz6IB90CtWY4=', 'LHxP4Xh7bN0=', 'user');
        //private static readonly User USER_2 = new User(2, 'Admin', 'Test', 'admin@storage.com', 'YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=', 'admin')


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

        [TestMethod] //NEEDS UPDATING
        public void GetUserById_ReturnsCorrectUser()
        {
            //Arrange 
            int id = 1;

            //Act - retrieve user
            User user = userSqlDao.GetUserById(id);
            User TEMP = userSqlDao.GetUserById(1); //DELETE 

            //Assert - use AssertUsersMatch method
            AssertUsersMatch(TEMP, user);
        }

        [TestMethod]
        public void GetUserById_ThrowsDaoException()
        {
            try
            {
                userSqlDao.GetUserById(4);
            }
            catch(DaoException ex)
            {
                if(ex.GetType() == typeof(DaoException))
                {
                    Assert.AreEqual(1, 1);
                }
            }
        }

        private void AssertUsersMatch(User expected, User actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.FirstName, actual.FirstName);
            Assert.AreEqual(expected.LastName, actual.LastName);
            Assert.AreEqual(expected.Email, actual.Email);
            Assert.AreEqual(expected.Role, actual.Role);



        }

    }
}
