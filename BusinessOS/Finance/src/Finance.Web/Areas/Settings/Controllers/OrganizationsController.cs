using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Finance.Web.Areas.Settings.Controllers
{
    [Area("Settings")]
    public class OrganizationsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}