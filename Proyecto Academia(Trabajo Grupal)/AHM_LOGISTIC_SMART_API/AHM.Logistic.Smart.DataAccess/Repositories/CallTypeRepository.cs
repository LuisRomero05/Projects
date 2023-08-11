using AHM.Logistic.Smart.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.DataAccess.Repositories
{
    public class CallTypeRepository : IRepository<tbCallType, View_tbCallType_List>
    {
        public int Delete(int Id, int Mod)
        {
            throw new NotImplementedException();
        }

        public tbCallType Find(Expression<Func<tbCallType, bool>> expression = null)
        {
            throw new NotImplementedException();
        }

        public int Insert(tbCallType item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<View_tbCallType_List> List()
        {
            using var db = new LogisticSmartContext();
            return db.View_tbCallType_List.ToList();
        }

        public View_tbCallType_List Search(Expression<Func<View_tbCallType_List, bool>> expression = null)
        {
            throw new NotImplementedException();
        }

        public int Update(int Id, tbCallType item)
        {
            throw new NotImplementedException();
        }
    }
}
