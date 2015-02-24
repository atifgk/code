using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace getranked.Models
{
    public class task
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime  due_date { get; set; }
        public string required_hours { get; set; }
        public string priority { get; set; }

    }
}