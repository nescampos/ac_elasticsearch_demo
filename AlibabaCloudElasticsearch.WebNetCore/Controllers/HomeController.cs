using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AlibabaCloudElasticsearch.WebNetCore.Models;
using Nest;

namespace AlibabaCloudElasticsearch.WebNetCore.Controllers
{
    public class HomeController : Controller
    {
        private ConnectionSettings _settings = null;
        private ElasticClient _client = null;

        public HomeController()
        {
            _settings = new ConnectionSettings(new Uri("<PUBLIC_URL_FROM_ALIBABA>:<PUBLIC_PORT>"));
            _settings.BasicAuthentication("<USERNAME>", "<PASSWORD>");
            _client = new ElasticClient(_settings);
            
        }

        public IActionResult Index()
        {
            var indexesNames = _client.CatIndices().Records.Select(x => x.Index);
            return View(indexesNames);
        }

        public IActionResult CreateESIndex()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateESIndex(string indexName)
        {
            _client.CreateIndex(indexName);
            return RedirectToAction("Index");
        }

        

        public IActionResult Search(string indexName, string query)
        {
            SearchViewModel model = new SearchViewModel();
            SearchDescriptor<ESData> descriptor = new SearchDescriptor<ESData>();
            descriptor.Index(indexName);
            descriptor.Query(x => x.Match(qs => qs.Query(query)));
            var response = _client.Search<ESData>(descriptor);
            model.Respuesta = response.Documents;
            model.IndexName = indexName;
            return View(model);
        }

        public IActionResult AddData(string indexName)
        {
            return View("AddData", indexName);
        }

        [HttpPost]
        public IActionResult AddData(string indexName, string FirstName, string LastName ,DateTime DateOfBirth,string Role, string Department,int YearsInRole,string Company)
        {
            ESData data = new ESData { Company = Company, DateOfBirth = DateOfBirth, Department = Department, FirstName = FirstName, LastName = LastName, Role = Role, YearsInRole = YearsInRole };
            _client.Index(data, x => x.Index(indexName));
            return RedirectToAction("Search", new { indexName = indexName });
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
