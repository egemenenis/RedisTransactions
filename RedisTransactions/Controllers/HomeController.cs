using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RedisTransactions.Services;
using System.Linq;
using RedisTransactions.Models;

namespace RedisProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITestService _testService;
        public HomeController(ILogger<HomeController> logger, ITestService testService)
        {
            _logger = logger;
            _testService = testService;
            _testService.Connection(5);
        }

        public async Task<IActionResult> Index()
        {
            //The desired database number can be entered
            _testService.Connection(1);

            //For set operation
            //await _testService.StringSet("data", "data1");

            //For get operation
            //var data = _testService.StringGet("data");

            //For delete operation
            //await _testService.KeyDelete("data");

            //***** FOR LIST TRANSACTIONS *****

            var db = _testService.Database();
            await db.ListLeftPushAsync("List", "Data1");
            await db.ListLeftPushAsync("List", "Data2");
            await db.ListLeftPushAsync("List", "Data3");
            await db.ListLeftPushAsync("List", "Data4");


            List<string> myList = new List<string>();
            if(await db.KeyExistsAsync("List"))
            {
                var redisList= await db.ListRangeAsync("List");
                redisList.ToList().ForEach(a =>
                {
                    myList.Add(a.ToString());
                });
            }
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
