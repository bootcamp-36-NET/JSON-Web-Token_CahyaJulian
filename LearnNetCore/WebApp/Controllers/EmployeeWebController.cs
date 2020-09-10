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
            //var token = HttpContext.Session.GetString("JWToken");
            //httpClient.DefaultRequestHeaders.Add("Authorization", token);
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
        public IActionResult GetById(int id)
        {
            EmployeeVM employ = null;
            //var token = HttpContext.Session.GetString("JWToken");
            //httpClient.DefaultRequestHeaders.Add("Authorization", token);
            var resTask = httpClient.GetAsync("employees/" + id);
            resTask.Wait();
            var result = resTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var getJson = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                employ = JsonConvert.DeserializeObject<EmployeeVM>(getJson);
            }

            return Json(employ, new Newtonsoft.Json.JsonSerializerSettings());
        }
        public IActionResult Delete(int id)
        {
            //var token = HttpContext.Session.GetString("JWToken");
            //httpClient.DefaultRequestHeaders.Add("Authorization", token);
            var result = httpClient.DeleteAsync("employees/" + id).Result;
            return Json(result);
        }
    }
}
