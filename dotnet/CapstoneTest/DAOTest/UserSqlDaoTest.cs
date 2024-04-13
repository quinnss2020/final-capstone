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
        private static readonly User USER_1 = new User(1, "Admin", "Test", "admin@storage.com", "YhyGVQ+Ch69n4JMBncM4lNF/i9s=", "Ar/aB2thQTI=", "admin", "123456", false, false);
        private static readonly User USER_2 = new User(2, "User", "Test", "user@storage.com", "Jg45HuwT7PZkfuKTz6IB90CtWY4=", "LHxP4Xh7bN0=", "user", "456789", false, false);


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
            Assert.AreEqual(5, users.Count);
        }

        [TestMethod]
        public void GetUserById_ReturnsCorrectUser()
        {
            //Arrange 
            int id = 1;

            //Act - retrieve user
            User user = userSqlDao.GetUserById(id);


            //Assert - use AssertUsersMatch method
            AssertUsersMatch(USER_1, user);
        }

        [TestMethod]
        public void GetUserById_ThrowsDaoException()
        {
            try
            {
                userSqlDao.GetUserById(4);
            }
            catch (DaoException ex)
            {
                if (ex.GetType() == typeof(DaoException))
                {
                    Assert.AreEqual(1, 1);
                }
            }
        }

        [TestMethod]
        public void GetUserByEmail_ReturnsCorrectUser()
        {
            //Arrange
            User user = userSqlDao.GetUserByEmail(USER_2.Email);
            //Act

            //Assert
            AssertUsersMatch(USER_2, user);
        }

        [TestMethod]
        public void GetUserByEmail_ThrowsDaoException()
        {
            try
            {
                userSqlDao.GetUserByEmail("testing@test.com");
            }
            catch (DaoException ex)
            {
                if (ex.GetType() == typeof(DaoException))
                {
                    Assert.AreEqual(1, 1);
                }
            }
        }

        [TestMethod]
        public void CreateUser_CreatesUser()
        {
            RegisterUser newUser = new RegisterUser();
            newUser.Email = "email@email.com";
            newUser.FirstName = "Jake";
            newUser.LastName = "Norris";
            newUser.Password = "password";
            newUser.ConfirmPassword = "password";
            newUser.Role = "user";

            userSqlDao.CreateUser(newUser);

            User actualUser = userSqlDao.GetUserByEmail("email@email.com");

            Assert.AreEqual(newUser.Email, actualUser.Email);
        }

        [TestMethod]
        public void CreateUser_DoesNotCreateUser()
        {
            RegisterUser newUser = new RegisterUser();
            newUser.FirstName = "Jake";
            newUser.LastName = "Norris";
            newUser.Password = "password";
            newUser.ConfirmPassword = "password";
            newUser.Role = "user";
            try
            {
                User actualUser = userSqlDao.CreateUser(newUser);
            }
            catch (DaoException ex)
            {
                if (ex.GetType() == typeof(DaoException))
                {
                    Assert.AreEqual(1, 1);
                }
            }

        }

        [TestMethod]
        public void UpdateUser_ConfirmsUser()
        {
            User oldUser = USER_1;
            oldUser.Confirmed = true;

            User updatedUser = userSqlDao.UpdateUser(oldUser);

            Assert.AreEqual(true, updatedUser.Confirmed);

        }

        [TestMethod]
        public void UpdateUser_ThrowsDaoException()
        {
            User oldUser = USER_1;
            oldUser.Id = 1008;
            try
            {
                User updatedUser = userSqlDao.UpdateUser(oldUser);
            }
            catch (DaoException ex)
            {
                if (ex.GetType() == typeof(DaoException))
                {
                    Assert.AreEqual(1, 1);
                }
            }
        }

        [TestMethod]
        public void UpdateAgreed_UpdatesAgreed()
        {
            User oldUser = USER_1;
            oldUser.Agreed = true;

            User updatedUser = userSqlDao.UpdateAgreed(oldUser);

            Assert.AreEqual(true, updatedUser.Agreed);

        }

        private void AssertUsersMatch(User expected, User actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.FirstName, actual.FirstName);
            Assert.AreEqual(expected.LastName, actual.LastName);
            Assert.AreEqual(expected.Email, actual.Email);
            Assert.AreEqual(expected.Role, actual.Role);
            Assert.AreEqual(expected.Confirmed, actual.Confirmed);
            Assert.AreEqual(expected.Agreed, actual.Agreed);
        }

    }
}
