using System.Collections.Generic;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface IUnitSqlDao
    {
        public IList<Unit> GetAllActiveUnits();
        public Unit GetUnitById(int id);
        public Unit UpdateUnit(Unit unit);
    }
}
