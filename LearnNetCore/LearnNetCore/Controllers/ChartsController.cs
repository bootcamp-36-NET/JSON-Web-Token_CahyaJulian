using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnNetCore.Context;
using LearnNetCore.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearnNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartsController : ControllerBase
    {
        private readonly MyContext _context;
        public ChartsController(MyContext myContext)
        {
            _context = myContext;
        }
        // GET api/values
        [HttpGet]
        [Route("pie")]
        public async Task<List<AmPieChartVM>> GetPie()
        {
            var datas = await _context.divisions.Include("Department")
                            .Where(x => x.isDelete == false)
                            .GroupBy(q => q.Department.Name)
                            .Select(q => new AmPieChartVM
                            {
                                DepartmentName = q.Key,
                                total = q.Count()
                            }).ToListAsync();
            return datas;
        }
    }
}
