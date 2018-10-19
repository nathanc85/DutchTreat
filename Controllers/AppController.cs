using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DutchTreat.Data;
using DutchTreat.Services;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DutchTreat.Controllers
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;
        private readonly DutchContext _ctx;

        public AppController(IMailService mailService, DutchContext ctx)
        {
            _mailService = mailService;
            _ctx = ctx;
        }

        public IActionResult Index()
        {
            var results = _ctx.Products.ToList();
            return View();
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            //throw new Exception("Bad bad info");
            //ViewBag.Title = "Contact Us";
            return View();
        }

        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Send email.
                _mailService.SendMessage("cnlnat@yahoo.com", model.Subject, $"From: {model.Name} - {model.Email}, Message: {model.Message}");
                ViewBag.UserMessage = "Email sent...";
                ModelState.Clear();
            }
            else
            {
                // Show errors.
            }

            return View();
        }

        public IActionResult About()
        {
            ViewBag.Title = "About Us";
            return View();
        }

        public IActionResult Shop()
        {
            //var results = _context.Products
            //.OrderBy(p => p.Category)
            //.ToList();
            // OR with LINQ
            var results = from p in _ctx.Products
                          orderby p.Category
                          select p;
            return View(results.ToList());
        }
    }
}

