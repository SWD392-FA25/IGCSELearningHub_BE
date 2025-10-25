using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Analytics
{
    public class RevenueSeriesDTO
    {
        public string SeriesName { get; set; } = "Revenue";
        public List<TimePointDTO> Points { get; set; } = new();
    }
}
