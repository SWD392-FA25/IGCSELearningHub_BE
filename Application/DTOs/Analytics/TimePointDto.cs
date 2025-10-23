using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Analytics
{
    public class TimePointDto
    {
        public int Year { get; set; }
        public int Month { get; set; }   // 1..12
        public int? Day { get; set; }    // null khi GroupBy=Month
        public decimal Value { get; set; }
    }
}
