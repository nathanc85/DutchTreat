using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DutchTreat.Controllers
{
    public class AppController : Controller
    {
        public IActionResult Index() {
            return View();
        }

        [HttpGet("contact")]
        public IActionResult Contact() {
            //throw new Exception("Bad bad info");
            //ViewBag.Title = "Contact Us";
            return View();
        }

        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model) {
            if (ModelState.IsValid) {
                // Send email.
            }
            else {
                // Show errors.
            }

            return View();
        }

        public IActionResult About() {
            ViewBag.Title = "About Us";
            return View();
        }
    }
}

