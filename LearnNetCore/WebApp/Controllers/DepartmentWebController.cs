using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using LearnNetCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebApp.Controllers
{
    public class DepartmentWebController : Controller
    {
        readonly HttpClient httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:44380/api/")
        };
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult LoadDepartments()
        {
            IEnumerable<Department> departments = null;
            var token = HttpContext.Session.GetString("JWToken");
            httpClient.DefaultRequestHeaders.Add("Authorization", token);
            var restTask = httpClient.GetAsync("Department");
            restTask.Wait();

            var result = restTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Department>>();
                readTask.Wait();
                departments = readTask.Result;

            }
            return Json(departments, new Newtonsoft.Json.JsonSerializerSettings());

        }
        //public async Task<Uri> CreateDepartmentAsync(Department department)
        //{
        //    HttpResponseMessage response = await httpClient.PostAsJsonAsync(
        //        "Department", department);
        //    response.EnsureSuccessStatusCode();

        //    // return URI of the created resource.
        //    return response.Headers.Location;
        //}
        public JsonResult InsertorupdateDepartment(Department departments, int id)
        {
            try
            {
                var json = JsonConvert.SerializeObject(departments);
                var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var token = HttpContext.Session.GetString("JWToken");
                httpClient.DefaultRequestHeaders.Add("Authorization", token);

                if (id == 0)
                {
                    var result = httpClient.PostAsync("department", byteContent).Result;
                    return Json(result);
                }
                else if (id != 0)
                {
                    var result = httpClient.PutAsync("department/" + id, byteContent).Result;
                    return Json(result);
                }

                return Json(404);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Department> GetDepartmentAsync(int id)
        {
            Department department = null;
            HttpResponseMessage response = await httpClient.GetAsync("id");
            if (response.IsSuccessStatusCode)
            {
                department = await response.Content.ReadAsAsync<Department>();
            }
            return department;
        }
        public JsonResult GetById(int id)
        {
            Department departments = null;
            var token = HttpContext.Session.GetString("JWToken");
            httpClient.DefaultRequestHeaders.Add("Authorization", token);
            var resTask = httpClient.GetAsync("department/" + id);
            resTask.Wait();
            var result = resTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var getJson = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                departments = JsonConvert.DeserializeObject<Department>(getJson);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server Error try after sometimes.");
            }

            return Json(departments, new Newtonsoft.Json.JsonSerializerSettings());
        }
        public async Task<HttpStatusCode> DeleteDepartmentAsync(int id)
        {
            HttpResponseMessage response = await httpClient.DeleteAsync(
                $"department/" + id);
            return response.StatusCode;
        }
        public IActionResult Delete(int id)
        {
            var token = HttpContext.Session.GetString("JWToken");
            httpClient.DefaultRequestHeaders.Add("Authorization", token);
            var result = httpClient.DeleteAsync("department/" + id).Result;
            return Json(result);
        }

    }
}
