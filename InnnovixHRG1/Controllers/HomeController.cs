using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InnnovixHRG1.Models;

namespace InnnovixHRG1.Controllers
{
    public class HomeController : Controller
    {
        Innovix12021DataContext mydb = new Innovix12021DataContext();

        // GET: Home
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Admin()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UserPage()
        {
            ViewBag.RoomTypeIDD = new SelectList(mydb.RoomTypeTBL, "RoomTypeID", "RoomTypeName");
            ViewBag.MealTypeIDD = new SelectList(mydb.MealTypeTBL, "MealTypeID", "MealTypeName");
            ViewBag.CountryIDD = new SelectList(mydb.CountryTBL, "CountryID", "CountryName");

            return View();
        }

        [HttpPost]
        public ActionResult UserPage(userinput mymodel)
        {
            int result = GetReservationTotal(mymodel);
            return RedirectToAction("Payment", new { costs = result});
        }

        [HttpGet]
        public ActionResult Payment(int costs)
        {
            ViewBag.cost = costs;
            return View();
        }


        public int GetReservationTotal (userinput model)
        {
            int result = 0;
            //DateTime myFirstDate = (DateTime)model.checkINdate;

            //
            while ( (DateTime)model.checkINdate <= (DateTime)model.checkOUTdate)
            {
                var gettingroom = mydb.RoomPriceTBL.Where(a => a.RoomTypeIDD == model.RoomTypeIDD).ToList();
                var Requiredroom = new RoomPriceTBL();
                foreach (var item in gettingroom)
                {
                    if (DatesComparison.DateEqualsORLessThanDate2((DateTime)item.SeasonPHolidayTBL.SeasonHolidayFrom, (DateTime)item.SeasonPHolidayTBL.SeasonHolidayTo, (DateTime)model.checkINdate))
                    {
                        if (DatesComparison.DateEqualsORLessThanDate2((DateTime)item.SeasonPHolidayTBL.SeasonHolidayFrom, (DateTime)item.SeasonPHolidayTBL.SeasonHolidayTo, (DateTime)model.checkOUTdate))
                        {
                            Requiredroom = item;
                        }
                    }
                }

                //
                var gettingmeal = mydb.MealPriceTBL.Where(a => a.MealTypeIDD == model.MealTypeIDD).ToList();
                var Requiredmeal = new MealPriceTBL();
                foreach (var item in gettingmeal)
                {
                    if (DatesComparison.DateEqualsORLessThanDate2((DateTime)item.SeasonPHolidayTBL.SeasonHolidayFrom, (DateTime)item.SeasonPHolidayTBL.SeasonHolidayTo, (DateTime)model.checkINdate))
                    {
                        if (DatesComparison.DateEqualsORLessThanDate2((DateTime)item.SeasonPHolidayTBL.SeasonHolidayFrom, (DateTime)item.SeasonPHolidayTBL.SeasonHolidayTo, (DateTime)model.checkOUTdate))
                        {
                            Requiredmeal = item;
                        }
                    }
                }


                int totalmealprice = (Convert.ToInt32(model.children) + Convert.ToInt32(model.adults)) * Convert.ToInt32(Requiredmeal.MealPriceNum);

                int totalroomprice = Convert.ToInt32(Requiredroom.RoomPriceNum);

                result += totalmealprice + totalroomprice;

                model.checkINdate = model.checkINdate.AddDays(1);
            }

            return result;
        }

        [HttpPost]
        public JsonResult GetResultConCurrent(DateTime checkINdate, DateTime checkOUTdate, string adults, string children, int RoomTypeIDD, int MealTypeIDD)
        {
            try
            {
                userinput mymodel = new userinput();
                mymodel.checkINdate = checkINdate;
                mymodel.checkOUTdate = checkOUTdate;
                mymodel.adults = adults;
                mymodel.children = children;
                mymodel.RoomTypeIDD = RoomTypeIDD;
                mymodel.MealTypeIDD = MealTypeIDD;
                int result = GetReservationTotal(mymodel);
                return Json(result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

    }
}