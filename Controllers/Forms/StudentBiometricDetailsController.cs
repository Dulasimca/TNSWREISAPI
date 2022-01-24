using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TNSWREISAPI.Controllers.Forms
{
    public class StudentBiometricDetailsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
