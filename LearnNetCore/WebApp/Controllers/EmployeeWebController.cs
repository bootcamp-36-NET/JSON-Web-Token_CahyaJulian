using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using LearnNetCore.Models;
using LearnNetCore.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebApp.Controllers
{
    public class EmployeeWebController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        readonly HttpClient httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:44380/api/")
        };
        public IActionResult LoadDivision()
        {
            IEnumerable<EmployeeVM> employee = null;
            var restTask = httpClient.GetAsync("employees");
            restTask.Wait();

            var result = restTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<EmployeeVM>>();
                readTask.Wait();
                employee = readTask.Result;
            }
            return Json(employee, new Newtonsoft.Json.JsonSerializerSettings());
        }
        public IActionResult Delete(string id)
        {
            var result = httpClient.DeleteAsync("employees/" + id).Result;
            return Json(result);
        }
    }
}
