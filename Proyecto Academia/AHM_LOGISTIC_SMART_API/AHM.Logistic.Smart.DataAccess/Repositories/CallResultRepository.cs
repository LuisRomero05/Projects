using AHM.Logistic.Smart.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.DataAccess.Repositories
{
    public class CallResultRepository : IRepository<tbCallResult, View_tbCallResult_List>
    {
        public int Delete(int Id, int Mod)
        {
            throw new NotImplementedException();
        }

        public tbCallResult Find(Expression<Func<tbCallResult, bool>> expression = null)
        {
            throw new NotImplementedException();
        }

        public int Insert(tbCallResult item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<View_tbCallResult_List> List()
        {
            using var db = new LogisticSmartContext();
            return db.View_tbCallResult_List.ToList();
        }

        public View_tbCallResult_List Search(Expression<Func<View_tbCallResult_List, bool>> expression = null)
        {
            throw new NotImplementedException();
        }

        public int Update(int Id, tbCallResult item)
        {
            throw new NotImplementedException();
        }
    }
}
