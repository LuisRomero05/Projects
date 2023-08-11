using AHM.Logistic.Smart.DataAccess.Context;
using AHM.Logistic.Smart.Entities.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.DataAccess.Repositories
{
    public class LlEventLog
    {
        public void Insert(tbEventLog eventLog)
        {
            using var db = new LogisticSmartContext();
            eventLog.event_CreationDate = DateTime.Now;
            db.tbEventLog.Add(eventLog);
            db.SaveChanges();
        }
    }
}
