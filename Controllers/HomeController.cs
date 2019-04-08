using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dojodachi.Models;
using Microsoft.AspNetCore.Http;

namespace Dojodachi.Controllers
{
    public class HomeController : Controller
    {
        Random rand = new Random();

        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            HttpContext.Session.SetInt32("Full", 20);
            HttpContext.Session.SetInt32("Hap", 20);
            HttpContext.Session.SetInt32("Energy", 50);
            HttpContext.Session.SetInt32("Meals", 3);
            HttpContext.Session.SetString("Message", "");

            ViewBag.Full = HttpContext.Session.GetInt32("Full");
            ViewBag.Hap = HttpContext.Session.GetInt32("Hap");
            ViewBag.Energy = HttpContext.Session.GetInt32("Energy");
            ViewBag.Meals = HttpContext.Session.GetInt32("Meals");
            ViewBag.Message = HttpContext.Session.GetString("Message");
            return View();
        }
        [Route("/game")]
        [HttpGet]
        public IActionResult Game()
        {
            int? Meals = HttpContext.Session.GetInt32("Meals");
            int? Energy = HttpContext.Session.GetInt32("Energy");
            int? Fullness = HttpContext.Session.GetInt32("Full");
            int? Happiness = HttpContext.Session.GetInt32("Hap");

            if (Energy >= 100 && Fullness >= 100 && Happiness >= 100)
            {
                string message = "You WON!";
                HttpContext.Session.SetString("Message", message);
            }
            if (Fullness <= 0 || Happiness <= 0)
            {
                string message = "You LOSE!";
                HttpContext.Session.SetString("Message", message);
            }
            if (Energy <= 0)
            {
                HttpContext.Session.SetInt32("Energy", 0);
                ViewBag.Energy = HttpContext.Session.GetInt32("Energy");
            }
            if (Meals <= 0)
            {
                HttpContext.Session.SetInt32("Meals", 0);
                ViewBag.Meals = HttpContext.Session.GetInt32("Meals");
            }


            ViewBag.Full = HttpContext.Session.GetInt32("Full");
            ViewBag.Hap = HttpContext.Session.GetInt32("Hap");
            ViewBag.Energy = HttpContext.Session.GetInt32("Energy");
            ViewBag.Meals = HttpContext.Session.GetInt32("Meals");
            ViewBag.Message = HttpContext.Session.GetString("Message");
            return View("Game");
        }

        [Route("/feed")]
        [HttpGet]
        public IActionResult Feed()
        {
            int? Meals = HttpContext.Session.GetInt32("Meals");
            Meals--;
            HttpContext.Session.SetInt32("Meals", (int)Meals);
            int? Full = HttpContext.Session.GetInt32("Full");
            int random = rand.Next(1, 6);
            Full += random;
            HttpContext.Session.SetInt32("Full", (int)Full);
            string message = "You feed your Dojodachi! Meals: -1; Fullness: +" + random;
            HttpContext.Session.SetString("Message", message);
            return RedirectToAction("Game");
        }

        [Route("/play")]
        [HttpGet]
        public IActionResult Play()
        {
            int? Energy = HttpContext.Session.GetInt32("Energy");
            Energy -= 5;
            HttpContext.Session.SetInt32("Energy", (int)Energy);
            int? Hap = HttpContext.Session.GetInt32("Hap");
            int random = rand.Next(1, 6);
            Hap += random;
            HttpContext.Session.SetInt32("Hap", (int)Hap);
            string message = "You played with your Dojodachi! Energy: -5; Happiness: +" + random;
            HttpContext.Session.SetString("Message", message);
            return RedirectToAction("Game");
        }
        [Route("/work")]
        [HttpGet]
        public IActionResult Work()
        {
            int? Energy = HttpContext.Session.GetInt32("Energy");
            Energy -= 5;
            HttpContext.Session.SetInt32("Energy", (int)Energy);
            int? Meals = HttpContext.Session.GetInt32("Meals");
            int random = rand.Next(1, 4);
            Meals += random;
            HttpContext.Session.SetInt32("Meals", (int)Meals);
            string message = "Your Dojodachi works! Energy: -5; Meal: +" + random;
            HttpContext.Session.SetString("Message", message);
            return RedirectToAction("Game");
        }
        [Route("/sleep")]
        [HttpGet]
        public IActionResult Sleep()
        {
            int? Energy = HttpContext.Session.GetInt32("Energy");
            Energy += 15;
            HttpContext.Session.SetInt32("Energy", (int)Energy);
            int? Hap = HttpContext.Session.GetInt32("Hap");
            Hap -= 5;
            HttpContext.Session.SetInt32("Hap", (int)Hap);
            int? Full = HttpContext.Session.GetInt32("Full");
            Full -= 5;
            HttpContext.Session.SetInt32("Full", (int)Full);
            string message = "ZZZZZzzzzzz Energy: +15; Happiness: -5; Fullness: -5";
            HttpContext.Session.SetString("Message", message);
            return RedirectToAction("Game");
        }
    }
}
