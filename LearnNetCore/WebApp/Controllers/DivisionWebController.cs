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
using OfficeOpenXml;
using WebApp.Report;
//using System.Data;

namespace WebApp.Controllers
{
    public class DivisionWebController : Controller
    {
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
        public JsonResult LoadChar()
        {
            IEnumerable<ChartVM> chart = null;
            //var token = HttpContext.Session.GetString("JWToken");
            //httpClient.DefaultRequestHeaders.Add("Authorization", token);
            var restTask = httpClient.GetAsync("employees/char");
            restTask.Wait();

            var result = restTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<ChartVM>>();
                readTask.Wait();
                chart = readTask.Result;
            }
            return Json(chart, new Newtonsoft.Json.JsonSerializerSettings());
            //return new JsonResult(chart);
        }
        public IActionResult ReportDivision()
        {
            DivisionReport divisionReport = new DivisionReport();
            List<Division> divisions = new List<Division>();
            var token = HttpContext.Session.GetString("JWToken");
            httpClient.DefaultRequestHeaders.Add("Authorization", token);
            var restTask = httpClient.GetAsync("division");
            restTask.Wait();

            var result = restTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<List<Division>>();
                readTask.Wait();
                divisions = readTask.Result;
            }

            byte[] abyte = divisionReport.PrepareReport(divisions);
            return File(abyte, "application/pdf");

        }
        //public List<Division> GetDivisions()
        //{
        //    List<Division> divisions = new List<Division>();
        //    Division division = new Division();
        //    foreach(var item in divisions)
        //    {
        //        Division division1 = new Division();
        //        division1.Name = "Division " + item.Name;
        //        division1.CreateDate = item.CreateDate;

        //        divisions.Add(division1);
        //    }
        //    return divisions;

        //}

        public IActionResult ReportExcel()
        {
            IEnumerable<Division> division = null;
            var token = HttpContext.Session.GetString("JWToken");
            httpClient.DefaultRequestHeaders.Add("Authorization", token);
            var restTask = httpClient.GetAsync("division");
            restTask.Wait();

            var res = restTask.Result;
            if (res.IsSuccessStatusCode)
            {
                var readTask = res.Content.ReadAsAsync<List<Division>>();
                readTask.Wait();
                division = readTask.Result;
            }

            var comlumHeadrs = new string[]
            {
                "No",
                "Id",
                "Division Name",
                "Department Name",
                "Create Date",
                "Update Date"
            };

            byte[] result;

            using (var package = new ExcelPackage())
            {
                // add a new worksheet to the empty workbook

                var worksheet = package.Workbook.Worksheets.Add("Current Division"); //Worksheet name
                using (var cells = worksheet.Cells[1, 1, 1, 5]) //(1,1) (1,5)
                {
                    cells.Style.Font.Bold = true;
                }

                //First add the headers
                for (var i = 0; i < comlumHeadrs.Count(); i++)
                {
                    worksheet.Cells[1, i + 1].Value = comlumHeadrs[i];
                }

                //Add values
                var number = 1;
                var j = 2;
                foreach (var divisions in division)
                {
                    worksheet.Cells["A" + j].Value = number++.ToString();
                    worksheet.Cells["B" + j].Value = divisions.Id;
                    worksheet.Cells["C" + j].Value = divisions.Name;
                    worksheet.Cells["D" + j].Value = divisions.Department.Name;
                    worksheet.Cells["E" + j].Value = divisions.CreateDate.ToString("MM/dd/yyyy");
                    worksheet.Cells["F" + j].Value = divisions.UpdateDate.ToString("MM/dd/yyyy");

                    j++;
                }
                result = package.GetAsByteArray();
            }

            return File(result, "application/ms-excel", $"Division.xlsx");
        }
    }
}
