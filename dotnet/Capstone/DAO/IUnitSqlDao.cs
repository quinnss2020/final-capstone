using System.Collections.Generic;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface IUnitSqlDao
    {
        public IList<Unit> GetAllUnits();
        public Unit GetUnitById(int id);
        public Unit UpdateUnit(Unit unit, int amount, int userId);
        public Unit UpdateUnitActive(Unit unit, bool active);
    }
}
