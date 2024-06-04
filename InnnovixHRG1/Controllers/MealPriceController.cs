using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InnnovixHRG1.Models;

namespace InnnovixHRG1.Controllers
{
    public class MealPriceController : Controller
    {
        // GET: MealPrice
        //public ActionResult Index()
        //{
        //    return View();
        //}
        Innovix12021DataContext mydb = new Innovix12021DataContext();


        [HttpPost]
        public JsonResult listAll(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;

                var lst = mydb.MealPriceTBL.ToList();
                return Json(new { Result = "OK", Records = lst.Skip(jtStartIndex).Take(jtPageSize), TotalRecordCount = lst.Count() });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex });
            }
        }


        [HttpPost]
        public JsonResult CreateOne(MealPriceTBL model)
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;

                if (ModelState.IsValid)
                {
                    //
                    var reqexist = mydb.MealPriceTBL.Where(a => a.MealTypeIDD == model.MealTypeIDD && a.SeasonHolidayIDD == model.SeasonHolidayIDD).Count();
                    if (reqexist == 0)
                    {
                        var data = mydb.MealPriceTBL.Add(model);
                        mydb.SaveChanges();
                        return Json(new { Result = "OK", Record = data });
                    }
                    else
                    {
                        return Json(new { Result = "ERROR", Message = "There is a record with the same name" });
                    }
                }
                else
                {
                    return Json(new { Result = "ERROR", Message = "Sorry, there is a missing in the DB" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex });

            }
        }


        [HttpPost]
        public JsonResult UpdateOne(MealPriceTBL model)
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;

                if (ModelState.IsValid)
                {
                    //check if  exist
                    //
                    //
                    var reqexist = mydb.MealPriceTBL.Where(a => a.MealPriceID != model.MealPriceID && a.MealTypeIDD == model.MealTypeIDD && a.SeasonHolidayIDD == model.SeasonHolidayIDD ).Count();
                    if (reqexist == 0)
                    {
                        var data = mydb.MealPriceTBL.Find(model.MealPriceID);
                        data.MealTypeIDD = model.MealTypeIDD;
                        data.SeasonHolidayIDD = model.SeasonHolidayIDD;
                        data.MealPriceNum = model.MealPriceNum;
                        data.IsStopped = model.IsStopped;
                        mydb.Entry(data).State = EntityState.Modified;
                        mydb.SaveChanges();
                        return Json(new { Result = "OK", Record = data });
                    }
                    else
                    {
                        return Json(new { Result = "ERROR", Message = "There is a record with the same name" });
                    }
                }
                else
                {
                    return Json(new { Result = "ERROR", Message = "Sorry, there is a missing in the DB" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex });
            }
        }


        [HttpPost]
        public JsonResult DeleteOne(int MealPriceID)
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;
                var newobject = mydb.MealPriceTBL.Find(MealPriceID);
                // Send row to be deleted
                if (newobject != null)
                {
                    var data = mydb.MealPriceTBL.Remove(newobject);
                    mydb.SaveChanges();
                    return Json(new { Result = "OK" });
                }
                else
                {
                    return Json(new { Result = "ERROR", Message = "Sorry, there is a missing in the DB" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex });
            }
        }




        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                mydb.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}