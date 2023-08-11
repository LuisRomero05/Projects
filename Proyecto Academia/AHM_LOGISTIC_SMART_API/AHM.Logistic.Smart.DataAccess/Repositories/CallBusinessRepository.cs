using AHM.Logistic.Smart.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.DataAccess.Repositories
{
    public class CallBusinessRepository : IRepository<tbCallBusiness, View_tbCallBusiness_List>
    {
        public int Delete(int Id, int Mod)
        {
            throw new NotImplementedException();
        }

        public tbCallBusiness Find(Expression<Func<tbCallBusiness, bool>> expression = null)
        {
            throw new NotImplementedException();
        }

        public int Insert(tbCallBusiness item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<View_tbCallBusiness_List> List()
        {
            using var db = new LogisticSmartContext();
            return db.View_tbCallBusiness_List.ToList();
        }

        public View_tbCallBusiness_List Search(Expression<Func<View_tbCallBusiness_List, bool>> expression = null)
        {
            throw new NotImplementedException();
        }

        public int Update(int Id, tbCallBusiness item)
        {
            throw new NotImplementedException();
        }
    }
}
