using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Restaurant.Controllers
{
    public class TableLoginController : Controller
    {
        RestaurantEntities db = new RestaurantEntities();
        public ActionResult Login(string tbcode, string pw)
        {
            string sql = "select * from Tables where TbCode = @tbcode and Password=@pw";
            List<SqlParameter> list = new List<SqlParameter>
            {
                new SqlParameter("tbcode",tbcode),
                new SqlParameter("pw",pw)
            };
            GetData gd = new GetData();

            var rd = gd.LoginQuery(sql, list);

            if (rd == null)
            {
                return RedirectToAction("Index", "Tables");
            }

            if (rd.HasRows)
            {                
             Session["TbID"] = rd["TbID"];
             rd.Close();
             return RedirectToAction("Index", "Menu", new { tbid = Session["TbID"] });
               
            }
            else
            {
                ViewBag.ErrMsg = "Please use correct table number to order";
            }
            rd.Close();
            return RedirectToAction("Index", "Tables");
        }
    }
}