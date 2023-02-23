//using Azure.Core;
using MetaData.Data;
using MetaData.Methods;
using MetaData.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MetaData.Controllers
{
    public class HomeController : Controller
    {
        //Dependency Injection
        private readonly ApplicationDbContext _db;
        private readonly ErrorLogs _errorLogs;

        public HomeController(
            ApplicationDbContext db,
            ErrorLogs errorLogs
            )
        {
            _db = db;
            _errorLogs = errorLogs;
        }

        public IActionResult Index()
        {
            //var remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;

            return View();
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
