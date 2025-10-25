using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Analytics
{
    public class CountSeriesDTO
    {
        public string SeriesName { get; set; } = "Count";
        public List<CountPointDTO> Points { get; set; } = new();
    }
}
