using getranked.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace getranked.infrastructure
{
    public class taskdb : DbContext, taskDatasource
    {
        public taskdb()
            : base("constring")
        {

        }
        public DbSet<task> task { get; set; }

        IQueryable<task> taskDatasource.task
        {
            get { return task; }
        }
    }
}