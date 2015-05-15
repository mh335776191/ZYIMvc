using FuLife.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZYiMvc.Models;

namespace ZYiMvc.Controllers
{
    public class HomeController : Controller
    {
        private DBContext db = new DBContext();
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult Register()
        {

            return View();
        }
        public ActionResult Buy()
        {

            return View();
        }
        /// <summary>
        /// 获取加工订单列表
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult getBuyList(int count)
        {
            dynamic result = "";
            result = (from i in db.Buy.Where(m => m.Status == "1").OrderByDescending(m => m.ModifyDate).OrderByDescending(m => m.IsTop).Take(count).AsEnumerable()
                      select new
                      {
                          i.ID,
                          i.Content,
                          i.Title,
                          i.Quantity,
                          CreateDate = i.CreateDate.ToString("MM.dd")
                      }).ToList();
            return Json(result);
        }
        /// <summary>
        /// 获取加急加工订单列表
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult getUrBuyList(int count)
        {
            dynamic result = "";
            result = (from i in db.Buy.Where(m => m.Status == "1" && m.IsUrgent == "1").OrderByDescending(m => m.ModifyDate).OrderByDescending(m => m.IsTop).Take(count).AsEnumerable()
                      select new
                      {
                          i.ID,
                          i.Title,
                          CreateDate = i.CreateDate.ToString("MM.dd")
                      }).ToList();
            return Json(result);
        }

        /// <summary>
        /// 获取加工订单列表(带分页)
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="page"></param>
        /// <param name="sidx"></param>
        /// <param name="sord"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult getBuyListP(int rows, int page, string sidx, string sord)
        {
            dynamic result = "";

            var objContext = ((IObjectContextAdapter)db).ObjectContext;
            var all = objContext.CreateObjectSet<Buy>();
            //排序
            var whereList = all.OrderBy(string.Format("it.{0} {1}", sidx, sord)).AsQueryable();

            whereList = whereList.Where(m => m.Status == "1" && m.IsUrgent == "1").OrderByDescending(m => m.ModifyDate).OrderByDescending(m => m.IsTop);

            var records = whereList.Count();
            result = new
            {
                rows =
                (from i in whereList.Skip((page - 1) * rows).Take(rows).ToList()
                 select new
                 {
                     i.ID,
                     i.Title,
                     i.Img1,
                     i.Click,
                     i.Content,
                     ModifyDate = i.ModifyDate.ToString("yyyy-MM-dd HH:mm:ss"),
                     CreateDate = i.CreateDate.ToString("yyyy-MM-dd")
                 }),

                records = records,
                page = page,
                total = records / rows + (records % rows != 0 ? 1 : 0)
            };

            return Json(result);
        }

        [HttpPost]
        public JsonResult getBuyByID(int id)
        {
            dynamic result = "";
            var i = db.Buy.FirstOrDefault(m => m.ID == id);
            if (i != null)
            {
                result = new
                {
                    i.ID,
                    i.Title,
                    Name = i.Member.Name,
                    Mobile=i.Member.Mobile,
                    Phone=i.Member.Phone,
                    Email=i.Member.Email,
                    Address=i.Member.Address,
                    QQ=i.Member.QQ,
                    CompanyName=i.Member.CompanyName,
                    i.Type,
                    i.Category,
                    i.Content,
                    i.Quantity,
                    i.Unit,
                    i.Price,
                    i.Img1,
                    i.Img2,
                    i.Img3,
                    CreateDate=i.CreateDate.ToString("yyyy-MM-dd"),
                    ValidityDate=i.ValidityDate.ToString("yyyy-MM-dd")
                };
            }
            return Json(result);
        }
        public ActionResult Sell()
        {
            return View();
        }
        /// <summary>
        /// 获取提供加工列表
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult getSellList(int count)
        {
            dynamic result = "";
            result = (from i in db.Sell.Where(m => m.Status == "1").OrderByDescending(m => m.ModifyDate).Take(count).AsEnumerable()
                      select new
                      {
                          i.ID,
                          i.Title,
                          CreateDate = i.CreateDate.ToString("MM.dd")
                      }).ToList();
            return Json(result);
        }

        /// <summary>
        /// 获取提供加工列表(带分页)
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="page"></param>
        /// <param name="sidx"></param>
        /// <param name="sord"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult getSellListP(int rows, int page, string sidx, string sord)
        {
            dynamic result = "";

            var objContext = ((IObjectContextAdapter)db).ObjectContext;
            var all = objContext.CreateObjectSet<Sell>();
            //排序
            var whereList = all.OrderBy(string.Format("it.{0} {1}", sidx, sord)).AsQueryable();

            whereList = whereList.Where(m => m.Status == "1").OrderByDescending(m => m.ModifyDate).OrderByDescending(m => m.IsTop);

            var records = whereList.Count();
            result = new
            {
                rows =
                (from i in whereList.Skip((page - 1) * rows).Take(rows).ToList()
                 select new
                 {
                     i.ID,
                     i.Title,
                     i.Img1,
                     i.Click,
                     i.Content,
                     ModifyDate = i.ModifyDate.ToString("yyyy-MM-dd HH:mm:ss"),
                     CreateDate = i.CreateDate.ToString("yyyy-MM-dd")
                 }),

                records = records,
                page = page,
                total = records / rows + (records % rows != 0 ? 1 : 0)
            };

            return Json(result);
        }
        public ActionResult News()
        {

            return View();
        }
        public ActionResult NewsDetail()
        {

            return View();
        }
        public ActionResult BuyDetail()
        {

            return View();
        }
        public ActionResult SellDetail()
        {

            return View();
        }
        public ActionResult Company()
        {

            return View();
        }
        public ActionResult Show()
        {

            return View();
        }

    }
}
