using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyContosoUniversity.Web.Data;
using MyContosoUniversity.Web.Models;
using MyContosoUniversity.Web.ViewModels;

namespace MyContosoUniversity.Web.Pages.Instructors
{
    public class InstructorCoursesPageModel : PageModel
    {
        public List<AssignedCourseViewModel> AssignedCourseDataList;

        public void PopulateAssignedCourseData(SchoolContext context,
            Instructor instructor)
        {
            var allCourses = context.Courses;

            var instructorCourses = new HashSet<int>(
                instructor.Courses.Select(c => c.CourseID)
            );

            AssignedCourseDataList = new List<AssignedCourseViewModel>();

            foreach (var course in allCourses)
            {
                AssignedCourseDataList.Add(new AssignedCourseViewModel
                {
                    CourseID = course.CourseID,
                    Title = course.Title,
                    Assigned = instructorCourses.Contains(course.CourseID)
                });
            }
        }
    }
}
