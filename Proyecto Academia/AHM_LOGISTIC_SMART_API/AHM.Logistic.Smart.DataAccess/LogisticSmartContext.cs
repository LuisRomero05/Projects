using AHM.Logistic.Smart.DataAccess.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.DataAccess
{
    public class LogisticSmartContext : LOGISTIC_SMART_AHMContext
    {
        public static string ConnectionString { get; set; }
        public LogisticSmartContext()
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        public LogisticSmartContext(string connection)
        {
            LogisticSmartContext.ConnectionString = connection;
            ChangeTracker.LazyLoadingEnabled = false;

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
            base.OnConfiguring(optionsBuilder);
        }
        public static void BuildConnectionString(string connection)
        {
            var connectionStringBuilder = new SqlConnectionStringBuilder { ConnectionString = connection };
            ConnectionString = connectionStringBuilder.ConnectionString;
        }
    }
}
