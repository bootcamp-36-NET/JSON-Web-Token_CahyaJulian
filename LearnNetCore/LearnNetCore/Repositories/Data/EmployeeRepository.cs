using LearnNetCore.Context;
using LearnNetCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnNetCore.Repositories.Data
{
    //public class EmployeeRepository : GeneralRepository<Employee, MyContext>
    public class EmployeeRepository
    {
        private readonly MyContext context;
        public EmployeeRepository(MyContext myContext)
        {
            this.context = myContext;
        }
        //public override async Task<List<Employee>> GetAll()
        //{
        //    var getDatas = await this.context.employees.Include("Users").ToListAsync();
        //    List<Employee> List = new List<Employee>();
        //    foreach (var employee in getDatas)
        //    {
        //        Employee emp = new Employee()
        //        {
        //            Id = Convert.ToInt32(employee.Users.Id),
        //            //UserId = employee.Users.Id,
        //            Phone = employee.Users.PhoneNumber,
        //            Address = employee.Address,
        //            Name = employee.Users.UserName,
        //            CreateDate = employee.CreateDate,
        //            UpdateDate = employee.UpdateDate,
        //            DeleteDate = employee.DeleteDate
        //        };
        //        List.Add(emp);
        //    }
        //    return getDatas;
        //}
    }
}
