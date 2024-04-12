using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Capstone.DAO;
using Capstone.Models;
using Capstone.Exceptions;

namespace Tutorial.Tests.DAO
{
    [TestClass]
    public class UnitSqlDaoTest
    {
        private static readonly Unit UNIT_1 = new Unit(1006, 117, 100, 100, 3, "AA1000", "Columbus", "5x5", true, new DateTime(2024, 04, 15, 10, 30, 50), new DateTime(2024, 04, 12, 10, 30, 50));

        private static readonly Unit UNIT_2 = new Unit(1007, 117, 100, 100, 3, "AA1000", "Columbus", "5x5", true, new DateTime(2024, 04, 15, 10, 30, 50), new DateTime(2024, 04, 12, 10, 30, 50));
    }
}
