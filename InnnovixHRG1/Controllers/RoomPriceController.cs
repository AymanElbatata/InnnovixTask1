using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InnnovixHRG1.Models;

namespace InnnovixHRG1.Controllers
{
    public class RoomPriceController : Controller
    {
        // GET: RoomPrice
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

                var lst = mydb.RoomPriceTBL.ToList();
                return Json(new { Result = "OK", Records = lst.Skip(jtStartIndex).Take(jtPageSize), TotalRecordCount = lst.Count() });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex });
            }
        }


        [HttpPost]
        public JsonResult CreateOne(RoomPriceTBL model)
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;

                if (ModelState.IsValid)
                {
                    //
                    var reqexist = mydb.RoomPriceTBL.Where(a => a.RoomTypeIDD == model.RoomTypeIDD && a.SeasonHolidayIDD == model.SeasonHolidayIDD).Count();
                    if (reqexist == 0)
                    {
                        var data = mydb.RoomPriceTBL.Add(model);
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
        public JsonResult UpdateOne(RoomPriceTBL model)
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;

                if (ModelState.IsValid)
                {
                    //check if  exist
                    //
                    //
                    var reqexist = mydb.RoomPriceTBL.Where(a => a.RoomPriceID != model.RoomPriceID && a.RoomTypeIDD == model.RoomTypeIDD && a.SeasonHolidayIDD == model.SeasonHolidayIDD).Count();
                    if (reqexist == 0)
                    {
                        var data = mydb.RoomPriceTBL.Find(model.RoomPriceID);
                        data.RoomTypeIDD = model.RoomTypeIDD;
                        data.SeasonHolidayIDD = model.SeasonHolidayIDD;
                        data.RoomPriceNum = model.RoomPriceNum;
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
        public JsonResult DeleteOne(int RoomPriceID)
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;
                var newobject = mydb.RoomPriceTBL.Find(RoomPriceID);
                // Send row to be deleted
                if (newobject != null)
                {
                    var data = mydb.RoomPriceTBL.Remove(newobject);
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