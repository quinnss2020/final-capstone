using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Capstone.DAO;
using Capstone.Models;
using Capstone.Exceptions;
using System.Runtime.CompilerServices;

namespace Tutorial.Tests.DAO
{
    [TestClass]
    public class UnitSqlDaoTest : BaseDaoTests
    {
        private static readonly Unit UNIT_1 = new Unit(1000, 202, 85, 85, 4, "II0008", "Columbus", "5x5", true, new DateTime(2024, 04, 18, 18, 08, 00), new DateTime(2024, 04, 13, 12, 00, 00), "Kitchen odds and ends: Scratched cookware, outdated appliances, and half-used ingredients, a hodgepodge of culinary essentials perfect for those who appreciate the charm of well-loved kitchenware and budget-friendly cooking.");

        private static readonly Unit UNIT_2 = new Unit(1006, 118, 200, 200, 3, "AA1001", "Cleveland", "10x10", false, new DateTime(2024, 04, 15, 10, 30, 50), new DateTime(2024, 04, 12, 10, 30, 50), "this is also a test unit");

        private UnitSqlDao unitDao;

        [TestInitialize]
        public override void Setup()
        {
            unitDao = new UnitSqlDao(ConnectionString);
            base.Setup();
        }

        [TestMethod]
        public void GetAllUnits()
        {
            IList<Unit> units = unitDao.GetAllUnits();

            Assert.IsNotNull(units);
            Assert.AreEqual(4, units.Count);
        }

        [TestMethod]
        public void GetUnitById_ReturnCorrectUnit()
        {
            Unit unit = unitDao.GetUnitById(1000);

            AssertUnitsMatch(UNIT_1, unit);
        }

        private void AssertUnitsMatch(Unit expected, Unit actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.LocalId, actual.LocalId);
            Assert.AreEqual(expected.StartBid, actual.StartBid);
            Assert.AreEqual(expected.HighestBid, actual.HighestBid);
            Assert.AreEqual(expected.HighestBidder, actual.HighestBidder);
            Assert.AreEqual(expected.OrderNumber, actual.OrderNumber);
            Assert.AreEqual(expected.City, actual.City);
            Assert.AreEqual(expected.Size, actual.Size);
            Assert.AreEqual(expected.Active, actual.Active);
            Assert.AreEqual(expected.Expiration, actual.Expiration);
            Assert.AreEqual(expected.Created, actual.Created);
        }
    }
}
