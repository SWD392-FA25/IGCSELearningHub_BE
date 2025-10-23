using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Packages
{
    public class PackageAddCoursesDTO
    {
        public List<int> CourseIds { get; set; } = new();
    }
}
