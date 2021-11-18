using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiftSite.Controllers
{
    public class AbminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


    }
}
