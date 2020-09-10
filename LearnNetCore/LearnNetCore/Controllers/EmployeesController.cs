using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnNetCore.Context;
using LearnNetCore.Models;
using LearnNetCore.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LearnNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly MyContext _context;
        public EmployeesController(MyContext myContext, IConfiguration configuration)
        {
            this._configuration = configuration;
            this._context = myContext;
        }
        [HttpGet]
        public async Task<List<EmployeeVM>> GetAll()
        {
            var getData = await this._context.employees.Include("Users").ToListAsync();
            List<EmployeeVM> list = new List<EmployeeVM>();
            foreach (var employee in getData)
            {
                EmployeeVM emp = new EmployeeVM()
                {
                    EmployeeId = employee.Users.Id,
                    EmployeeName = employee.Users.UserName,
                    Address = employee.Address,
                    Phone = employee.Users.PhoneNumber,
                    CreateDate = employee.CreateDate,
                    UpdateDate = employee.UpdateDate,
                    DeleteDate = employee.DeleteDate
                };
                list.Add(emp);
            }
            return list;
        }

        [HttpGet("{id}")]
        public EmployeeVM GetId(string id)
        {
            var getData = _context.employees.Include("Users").SingleOrDefault(x => x.Id == id);
            EmployeeVM emp = new EmployeeVM()
            {
                EmployeeId = getData.Users.Id,
                EmployeeName = getData.Users.UserName,
                Address = getData.Address,
                Phone = getData.Users.PhoneNumber,
                CreateDate = getData.CreateDate,
                UpdateDate = getData.UpdateDate,
                DeleteDate = getData.DeleteDate
            };
            return emp;
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if (id != null)
            {
                var getData = _context.employees.Include("Users").SingleOrDefault(x => x.Id == id);
                if (getData == null)
                {
                    return BadRequest("Not Seccessfully");
                }

                getData.DeleteDate = DateTimeOffset.Now;
                getData.isDelete = true;


                _context.Entry(getData).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok("Successfully Delete");
            }
            return Ok("Delete Failed");

        }
    }
}
