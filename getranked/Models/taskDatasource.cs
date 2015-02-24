using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace getranked.Models
{
    public interface taskDatasource
    {
        IQueryable<task> task { get; }
    }
}