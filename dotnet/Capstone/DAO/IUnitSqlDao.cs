using System.Collections.Generic;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface IUnitSqlDao
    {
        public IList<Unit> GetAllActiveUnits();
        public IList<Unit> GetAllInactiveUnits();
        public IList<Unit> GetAllUnits();
        public Unit GetUnitById(int id);
        public Unit UpdateUnit(Unit unit);
        public Unit DeleteUnit(int unitId);
    }
}
