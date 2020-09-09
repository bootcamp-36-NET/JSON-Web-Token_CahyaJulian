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
    public class DepartmentController : BasesController<Department, DepartmentRepository>
    {
        private readonly DepartmentRepository _departmentRepository;
        public DepartmentController(DepartmentRepository departmentRepo): base(departmentRepo)
        {
            this._departmentRepository = departmentRepo;
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult> Update(int id, Department entity)
        {
            var upd = await _departmentRepository.GetById(id);
            if (upd != null)
            {
                upd.Name = entity.Name;
                upd.CreateDate = DateTimeOffset.Now;
                upd.UpdateDate = DateTimeOffset.Now;
                await _departmentRepository.Update(upd);
                return Ok("data has been updated");
            }
            return BadRequest("Failed to update data. Please try again.");
        }
    }
}
