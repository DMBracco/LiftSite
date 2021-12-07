using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiftSite.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult BrandsTablePartial()
        {
            return PartialView("~/Views/Brands/BrandsTablePartial.cshtml");
        }
        public ActionResult TypeEquListPartial(int id)
        {
            return PartialView("~/Views/Projects/TypeEquListPartial.cshtml");
        }
        public ActionResult ImagesListPartial(int id)
        {
            return PartialView("~/Views/Projects/ImagesListPartial.cshtml");
        }
        public ActionResult EquipmentsListPartial(int id)
        {
            return PartialView("~/Views/Projects/EquipmentsListPartial.cshtml");
        }
    }
}
