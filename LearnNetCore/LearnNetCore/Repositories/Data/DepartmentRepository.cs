using LearnNetCore.Context;
using LearnNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnNetCore.Repositories.Data
{
    public class DepartmentRepository : GeneralRepository<Department, MyContext>
    {
        public DepartmentRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
