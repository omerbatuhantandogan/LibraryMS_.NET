﻿using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace BLL.Controllers.Bases
{
    public abstract class MvcController : Controller
    {
        protected MvcController()
        {
            CultureInfo cultureInfo = new CultureInfo("en-US");  //var cultureInfo = new CultureInfo("tr-TR");
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;

           
        }

    }
}
