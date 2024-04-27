using Demo.Models;
using Demo.Presentaion.Services.Classes;
using Demo.Presentaion.Services.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITranseantServices transeant1;
        private readonly ITranseantServices transeant2;
        private readonly IScopedServices scoped1;
        private readonly IScopedServices scoped2;
        private readonly ISingletonServices singleton1;
        private readonly ISingletonServices singleton2;

        public HomeController(ILogger<HomeController> logger
            , ITranseantServices transeant1
            , ITranseantServices transeant2
            , IScopedServices scoped1
            , IScopedServices scoped2
            , ISingletonServices singleton1
            , ISingletonServices singleton2
            )
        {
            _logger = logger;
            this.transeant1 = transeant1;
            this.transeant2 = transeant2;
            this.scoped1 = scoped1;
            this.scoped2 = scoped2;
            this.singleton1 = singleton1;
            this.singleton2 = singleton2;
        }

        public IActionResult Index()
        {
            return View();
        }
        public string TestLifeTime()
        {
            var builder = new StringBuilder();
            builder.Append($"transeant1 :: {transeant1}\n");
            builder.Append($"transeant1 :: {transeant2}\n\n");
            builder.Append($"Scoped 1 :: {scoped1}\n");
            builder.Append($"Scoped 2 :: {scoped2}\n\n");
            builder.Append($"singleton1 :: {singleton1}\n");
            builder.Append($"singleton2 :: {singleton2}\n\n");
            return builder.ToString();




        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
