using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InnnovixHRG1.Models;

namespace InnnovixHRG1.Controllers
{
    public class SeasonPHolidayController : Controller
    {
        // GET: SeasonPHoliday
        Innovix12021DataContext mydb = new Innovix12021DataContext();
        // GET: MealType
        //public ActionResult Index()
        //{
        //    return View();
        //}

        [HttpPost]
        public JsonResult listAll(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;

                var lst = mydb.SeasonPHolidayTBL.ToList();
                return Json(new { Result = "OK", Records = lst.Skip(jtStartIndex).Take(jtPageSize), TotalRecordCount = lst.Count() });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex });
            }
        }


        [HttpPost]
        public JsonResult CreateOne(SeasonPHolidayTBL model)
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;

                if (ModelState.IsValid)
                {
                    //
                    var reqexist = mydb.SeasonPHolidayTBL.Where(a => a.SeasonHolidayName == model.SeasonHolidayName).Count();
                    if (reqexist == 0)
                    {
                        var data = mydb.SeasonPHolidayTBL.Add(model);
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
        public JsonResult UpdateOne(SeasonPHolidayTBL model)
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;

                if (ModelState.IsValid)
                {
                    //check if  exist
                    //
                    //
                    var reqexist = mydb.SeasonPHolidayTBL.Where(a => a.SeasonHolidayID != model.SeasonHolidayID && a.SeasonHolidayName == model.SeasonHolidayName).Count();
                    if (reqexist == 0)
                    {
                        var data = mydb.SeasonPHolidayTBL.Find(model.SeasonHolidayID);
                        data.SeasonHolidayName = model.SeasonHolidayName;
                        data.SeasonHolidayFrom = model.SeasonHolidayFrom;
                        data.SeasonHolidayTo = model.SeasonHolidayTo;
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
        public JsonResult DeleteOne(int SeasonHolidayID)
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;
                var newobject = mydb.SeasonPHolidayTBL.Find(SeasonHolidayID);
                // Send row to be deleted
                if (newobject != null)
                {
                    var data = mydb.SeasonPHolidayTBL.Remove(newobject);
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


        [HttpPost]
        public JsonResult Get_SeasonHolidayIDD_ADDEDIT()
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;
                var continentals = mydb.SeasonPHolidayTBL.Where(a => a.IsStopped == false).Select(c => new CustomOption { DisplayText = c.SeasonHolidayName, Value = c.SeasonHolidayID }).ToList();
                CustomOption empty = new CustomOption();
                empty.Value = null;
                empty.DisplayText = "-";
                continentals.Insert(0, empty);
                return Json(new { Result = "OK", Options = continentals });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex });
            }
        }

        [HttpPost]
        public JsonResult Get_SeasonHolidayIDD_List()
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;
                var continentals = mydb.SeasonPHolidayTBL.Select(c => new CustomOption { DisplayText = c.SeasonHolidayName, Value = c.SeasonHolidayID }).ToList();
                return Json(new { Result = "OK", Options = continentals });
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