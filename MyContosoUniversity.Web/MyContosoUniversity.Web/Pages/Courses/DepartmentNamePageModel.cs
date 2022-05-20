using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyContosoUniversity.Web.Data;

namespace MyContosoUniversity.Web.Pages.Courses
{
    public class DepartmentNamePageModel : PageModel
    {
        public SelectList DepartmentNameSelectList { get; set; }

        public void PopulateDepartmentsDropDownList(
            SchoolContext context,
            object selectedDepartment = null)
        {
            var departmentsQuery = from d in context.Departments
                orderby d.Name // Sort by name.
                select d;

            DepartmentNameSelectList = new SelectList(
                departmentsQuery.AsNoTracking(),
                "DepartmentID", 
                "Name", 
                selectedDepartment);
        }
    }
}