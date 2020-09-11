using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using LearnNetCore.Context;
using LearnNetCore.Models;
using LearnNetCore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
//using System.Data;

namespace WebApp.Controllers
{
    public class DivisionWebController : Controller
    {
        private readonly MyContext mycontext;
        public IActionResult Index()
        {
            return View();
        }
        readonly HttpClient httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:44380/api/")
        };
        public JsonResult LoadDivision()
        {
            IEnumerable<Division> division = null;
            var token = HttpContext.Session.GetString("JWToken");
            httpClient.DefaultRequestHeaders.Add("Authorization", token);
            var restTask = httpClient.GetAsync("division");
            restTask.Wait();

            var result = restTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Division>>();
                readTask.Wait();
                division = readTask.Result;
            }
            return Json(division, new Newtonsoft.Json.JsonSerializerSettings());
        }
        public JsonResult InsertorupdateDivision(Division divisionVMs, int id)
        {
            try
            {
                var json = JsonConvert.SerializeObject(divisionVMs);
                var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var token = HttpContext.Session.GetString("JWToken");
                httpClient.DefaultRequestHeaders.Add("Authorization", token);

                if (divisionVMs.Id == 0)
                {
                    var result = httpClient.PostAsync("division", byteContent).Result;
                    return Json(result);
                }
                else if (divisionVMs.Id != 0)
                {
                    var result = httpClient.PutAsync("division/" + id, byteContent).Result;
                    return Json(result);
                }

                return Json(404);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public JsonResult GetById(int id)
        {
            Division divisionVMs = null;
            var token = HttpContext.Session.GetString("JWToken");
            httpClient.DefaultRequestHeaders.Add("Authorization", token);
            var resTask = httpClient.GetAsync("division/" + id);
            resTask.Wait();
            var result = resTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var getJson = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                divisionVMs = JsonConvert.DeserializeObject<Division>(getJson);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server Error. Please try again later");
            }

            return Json(divisionVMs, new Newtonsoft.Json.JsonSerializerSettings());
        }
        public JsonResult Delete(int id)
        {
            var token = HttpContext.Session.GetString("JWToken");
            httpClient.DefaultRequestHeaders.Add("Authorization", token);
            var result = httpClient.DeleteAsync("division/" + id).Result;
            return Json(result);
        }
        public async Task<ActionResult> LoadChar()
        {
            var dep = new Division();
            var date = await mycontext.divisions.Include("Departments").Select(j => j.DepartmentId == dep.DepartmentId).Distinct().ToListAsync();
            var success = mycontext.divisions
                .Where(j => j.DepartmentId == 6)
                .GroupBy(j => j.DepartmentId)
                .Select(group => new {
                    Department = group.Key,
                    Count = group.Count()
                });
            var countSuccess = success.Select(a => a.Count).ToArray();
            var exception = mycontext.divisions
                .Where(j => j.DepartmentId== 7)
                .GroupBy(j => j.DepartmentId)
                .Select(group => new {
                    Department = group.Key,
                    Count = group.Count()
                });
            var countException = exception.Select(a => a.Count).ToArray();
            return new JsonResult(new { myDate = date, mySuccess = countSuccess, myException = countException });
        }
    }
}
