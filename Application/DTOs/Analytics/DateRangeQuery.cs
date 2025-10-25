using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Analytics
{
    public class DateRangeQuery
    {
        public DateTime? From { get; set; }  // UTC
        public DateTime? To { get; set; }    // UTC
        public GroupBy GroupBy { get; set; } = GroupBy.Day;
    }
}
