﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnnovixHRG1.Controllers
{
    public class DatesComparison
    {

        public static bool DateEqualsORLessThanDate2(DateTime dateMin1, DateTime dateMax2, DateTime dateRow3)
        {
            
            if (dateMin1.Date == dateRow3.Date)
            {
               return true;
            }
            else if (dateMax2.Date == dateRow3.Date)
            {
                return true;
            }
            if (dateMin1 < dateRow3)
            {
                if (dateMax2 > dateRow3)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


    }
}