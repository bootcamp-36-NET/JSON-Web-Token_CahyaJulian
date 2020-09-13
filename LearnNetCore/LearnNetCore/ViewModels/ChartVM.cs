using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnNetCore.ViewModels
{
    public class ChartVM
    {
        public int CountA { get; set; }
        public int CountB { get; set; }
    }
    public class AmBarChartVM
    {
        public string date { get; set; }
        public string charts { get; set; }
        public string days { get; set; }
    }
    public class AmPieChartVM
    {
        public string DepartmentName { get; set; }
        public int total { get; set; }
    }
}
