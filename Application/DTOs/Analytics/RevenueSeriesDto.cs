using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Analytics
{
    public class RevenueSeriesDto
    {
        public string SeriesName { get; set; } = "Revenue";
        public List<TimePointDto> Points { get; set; } = new();
    }
}
