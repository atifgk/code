using getranked.infrastructure;
using getranked.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace getranked.Controllers
{
    public class taskController : Controller
    {
        //
        // GET: /task/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Getcontents()
        {
            string divdata = "";
            using (taskdb db = new taskdb())
            {
                var obj = (from a in db.task select a).ToList();
                foreach (var item in obj)
                {
                    divdata += "<tr >";
                    divdata += "<td class='ct' >" + item.title + "</td>";
                    divdata += "<td >" + item.description + "</td>";
                    divdata += "<td >" + item.due_date.ToString().Split(' ')[0] + "</td>";
                    divdata += "<td >" + item.priority + "</td>";
                    divdata += "<td class='cr'>" + item.required_hours + "</td>";
                    divdata += "<td>";
                    divdata += "<a title='Edit' id='" + (item.id) + "' class='btn btn-xs btn-green tableedit' href='#'>Edit</a>";
                    divdata += " <a title='Delete' id='btnDelete-" + (item.id) + "'  class='btn btn-xs btn-bricky tabledelete' href='#'>Delete</a>";
                    divdata += "</td>";
                    divdata += "</tr>";

                }

            }

            return Content(divdata, "text/html");

        }

        [HttpPost]
        public ActionResult delcontent(string idd)
        {
            using (taskdb db = new taskdb())
            {
                var cls = (from a in db.task where a.id.ToString() == idd select a).ToList();
                foreach (var item in cls)
                {
                    db.task.Remove(item);
                    db.SaveChanges();
                }
            }
            return Content("deleted", "text/html");
        }

        [HttpPost]
        public ActionResult getcontent(string idd)
        {
            string divdata = "";
            using (taskdb db = new taskdb())
            {

                var obj = (from ad in db.task where ad.id.ToString() == idd select ad);
                foreach (var item in obj)
                {
                    divdata = item.title + "`~`~`" + item.description + "`~`~`" + item.due_date + "`~`~`" + item.required_hours + "`~`~`" + item.priority;
                }

            }
            return Content(divdata, "text/html");

        }

        [HttpPost]
        public ActionResult updatecontent(string id, string title, string desc, string due_date, string required_hours, string priority)
        {
            string div = "";
            using (taskdb db = new taskdb())
            {
                try
                {
                    title = title.Replace("`~`~`", "''");
                    desc = desc.Replace("`~`~`", "''");
                    due_date = due_date.Replace("`~`~`", "''");
                    required_hours = required_hours.Replace("`~`~`", "''");
                    priority = priority.Replace("`~`~`", "''");

                    var cls = (from a in db.task where a.id.ToString() == id select a).Single();
                    cls.title = title;
                    cls.description = desc;
                    cls.due_date = DateTime.ParseExact(due_date, "d/M/yyyy", CultureInfo.InvariantCulture);
                    cls.required_hours = required_hours;
                    cls.priority = priority;
                    db.SaveChanges();
                    div = "added";
                }
                catch (Exception ex)
                {

                }
            }
            return Content(div, "text/html");

        }

        [HttpPost]
        public ActionResult addcontent(string title, string desc, string due_date, string required_hours, string priority)
        {
            string div = "";
            {
                using (taskdb db = new taskdb())
                {
                    int count = (from a in db.task where a.title == title select a).Count();
                    if (count == 0)
                    {
                        try
                        {                            

                            title = title.Replace("`~`~`", "''");
                            desc = desc.Replace("`~`~`", "''");
                            due_date = due_date.Replace("`~`~`", "''");
                            required_hours = required_hours.Replace("`~`~`", "''");
                            priority = priority.Replace("`~`~`", "''");

                            div = "added";
                            task obj = new task();
                            obj.title = title;
                            obj.description = desc;
                            obj.due_date = DateTime.ParseExact(due_date, "d/M/yyyy", CultureInfo.InvariantCulture);
                            obj.required_hours = required_hours;
                            obj.priority = priority;
                            db.task.Add(obj);
                            db.SaveChanges();

                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    else
                    {
                        div = "exist";
                    }

                }
                return Content(div, "text/html");
            }

        }
    }
}
