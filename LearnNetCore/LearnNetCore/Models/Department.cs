﻿using LearnNetCore.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LearnNetCore.Models
{
    [Table("departments")]
    public class Department : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset DeleteDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public bool isDelete { get; set; }
        public Department()
        {

        }
        public Department(Department department)
        {
            this.Name = department.Name;
            this.CreateDate = DateTimeOffset.Now;
            this.isDelete = false;
        }
        public void Update(Department department)
        {
            this.Name = department.Name;
            this.CreateDate = DateTimeOffset.Now;
        }
        public void Delete(Department department) {
            //this.Name = department.Name;
            this.DeleteDate = DateTimeOffset.Now;
            this.isDelete = true;
        }
    }
}
