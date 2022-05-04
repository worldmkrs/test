using Product.DataManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace Product.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string BrandID, string RegionID)
        {
            try
            {
                var results = HomeDataManager.GetHomeList(BrandID, RegionID);
                StringBuilder sb = new StringBuilder();
                foreach(var item in results)
                {
                    sb.Append(item.Order_id + " " + item.Order_cost + "" + item.Product + "" + item.Qty + "" + item.Brand_ID + "" + item.Region_ID);
                    sb.Append("\r\n");
                }
                return File(Encoding.ASCII.GetBytes(sb.ToString()), "Order_Details.XML");
            }
            catch(Exception e)
            {
                string error = e.Message;
                throw;
            }
        }

    }
}