using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InnnovixHRG1.Models;

namespace InnnovixHRG1.Controllers
{
    public class RoomTypeController : Controller
    {
        // GET: RoomType
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

                var lst = mydb.RoomTypeTBL.ToList();
                return Json(new { Result = "OK", Records = lst.Skip(jtStartIndex).Take(jtPageSize), TotalRecordCount = lst.Count() });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex });
            }
        }


        [HttpPost]
        public JsonResult CreateOne(RoomTypeTBL model)
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;

                if (ModelState.IsValid)
                {
                    //
                    var reqexist = mydb.RoomTypeTBL.Where(a => a.RoomTypeName == model.RoomTypeName).Count();
                    if (reqexist == 0)
                    {
                        var data = mydb.RoomTypeTBL.Add(model);
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
        public JsonResult UpdateOne(RoomTypeTBL model)
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;

                if (ModelState.IsValid)
                {
                    //check if  exist
                    //
                    //
                    var reqexist = mydb.RoomTypeTBL.Where(a => a.RoomTypeID != model.RoomTypeID && a.RoomTypeName == model.RoomTypeName).Count();
                    if (reqexist == 0)
                    {
                        var data = mydb.RoomTypeTBL.Find(model.RoomTypeID);
                        data.RoomTypeName = model.RoomTypeName;
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
        public JsonResult DeleteOne(int RoomTypeID)
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;
                var newobject = mydb.RoomTypeTBL.Find(RoomTypeID);
                // Send row to be deleted
                if (newobject != null)
                {
                    var data = mydb.RoomTypeTBL.Remove(newobject);
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
        public JsonResult Get_RoomTypeIDD_ADDEDIT()
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;
                var continentals = mydb.RoomTypeTBL.Where(a => a.IsStopped == false).Select(c => new CustomOption { DisplayText = c.RoomTypeName, Value = c.RoomTypeID }).ToList();
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
        public JsonResult Get_RoomTypeIDD_List()
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;
                var continentals = mydb.RoomTypeTBL.Select(c => new CustomOption { DisplayText = c.RoomTypeName, Value = c.RoomTypeID }).ToList();
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