using LearnNetCore.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LearnNetCore.Models
{
    [Table("tb_m_employees")]
    public class Employee 
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset DeleteDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public bool isDelete { get; set; }
        public User Users { get; set; }
    }
}
