using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Analytics
{
    public class CountSeriesDto
    {
        public string SeriesName { get; set; } = "Count";
        public List<CountPointDto> Points { get; set; } = new();
    }
}
