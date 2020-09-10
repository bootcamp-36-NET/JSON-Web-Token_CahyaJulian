using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnNetCore.Base;
using LearnNetCore.Models;
using LearnNetCore.Repositories.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearnNetCore.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    //public class EmployeeController : BasesController<Employee, EmployeeRepository>
    public class EmployeeController
    {
        private readonly EmployeeRepository _employeeRepository;
        public EmployeeController(EmployeeRepository employeeRepo)
        {
            this._employeeRepository = employeeRepo;
        }

        //[HttpPut("{Id}")]
        //public async Task<ActionResult> Update(int id, Employee entity)
        //{
        //    var upd = await _employeeRepository.GetById(id);
        //    if (upd != null)
        //    {
        //        upd.Name = entity.Name;
        //        upd.CreateDate = DateTimeOffset.Now;
        //        upd.UpdateDate = DateTimeOffset.Now;
        //        await _employeeRepository.Update(upd);
        //        return Ok("data has been updated");
        //    }
        //    return BadRequest("Failed to update data. Please try again.");
        //}
    }
}
