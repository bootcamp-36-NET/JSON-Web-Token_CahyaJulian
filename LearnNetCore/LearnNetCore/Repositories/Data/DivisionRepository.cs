using LearnNetCore.Context;
using LearnNetCore.Models;
using Microsoft.EntityFrameworkCore;
//using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnNetCore.Repositories.Data
{
    public class DivisionRepository : GeneralRepository<Division, MyContext>
    {
        private MyContext context;
        public DivisionRepository(MyContext myContext) : base(myContext)
        {
            this.context = myContext;
        }
        public override async Task<List<Division>> GetAll()
        {
            var getDatas = await this.context.divisions.Include("Department").Where(Q => Q.isDelete == false).ToListAsync();
            return getDatas;
        }
    }
}
