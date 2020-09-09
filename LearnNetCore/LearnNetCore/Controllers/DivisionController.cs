using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnNetCore.Base;
using LearnNetCore.Context;
using LearnNetCore.Models;
using LearnNetCore.Repositories.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearnNetCore.Controllers
{
    //[Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class DivisionController : BasesController<Division, DivisionRepository>
    {
        public readonly DivisionRepository _divisionRepository;
        public DivisionController(DivisionRepository divisionRepository): base(divisionRepository)
        {
            this._divisionRepository = divisionRepository;
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult> Update(int id, Division entity)
        {
            var upd = await _divisionRepository.GetById(id);
            if (upd != null)
            {
                upd.Name = entity.Name;
                upd.CreateDate = DateTimeOffset.Now;
                upd.UpdateDate = DateTimeOffset.Now;
                upd.DepartmentId = entity.DepartmentId;
                await _divisionRepository.Update(upd);
                return Ok("data has been updated");
            }
            return BadRequest("Failed to update data. Please try again.");
        }
        //[HttpGet("/GetAll")]
        //public override async Task<List<ActionResult>> GetSemua()
        //{
        //    var data = await myContext.divisions.Include("department").Where(Q => Q.isDelete == false).ToListAsync();
        //    return data;
        //}
    }
}
