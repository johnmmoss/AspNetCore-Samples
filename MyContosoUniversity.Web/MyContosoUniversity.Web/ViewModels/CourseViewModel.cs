using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyContosoUniversity.Web.ViewModels
{
    public class CourseViewModel
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        [DisplayName("Department Name")]
        public string DepartmentName { get; set; }
    }

}
