using AHM.Logistic.Smart.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.DataAccess.Repositories
{
    public class UnitsRepository : IRepository<tbUnits, View_tbUnits_List>
    {
        public int Delete(int Id, int Mod)
        {
            throw new NotImplementedException();
        }

        public tbUnits Find(Expression<Func<tbUnits, bool>> expression = null)
        {
            throw new NotImplementedException();
        }

        public int Insert(tbUnits item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<View_tbUnits_List> List()
        {
            using var db = new LogisticSmartContext();
            return db.View_tbUnits_List.ToList();
        }

        public View_tbUnits_List Search(Expression<Func<View_tbUnits_List, bool>> expression = null)
        {
            throw new NotImplementedException();
        }

        public int Update(int Id, tbUnits item)
        {
            throw new NotImplementedException();
        }
    }
}
