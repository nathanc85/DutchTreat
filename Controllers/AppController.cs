using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DutchTreat.Services;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DutchTreat.Controllers
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;

        public AppController(IMailService mailService)
        {
            _mailService = mailService;
        }

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
                _mailService.SendMessage("cnlnat@yahoo.com", model.Subject, $"From: {model.Name} - {model.Email}, Message: {model.Message}");
                ViewBag.UserMessage = "Email sent...";
                ModelState.Clear();
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

