using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyContosoUniversity.Web.Data;
using MyContosoUniversity.Web.Models;
using MyContosoUniversity.Web.ViewModels;

namespace MyContosoUniversity.Web.Pages.Instructors
{
    public class IndexModel : PageModel
    {
        private readonly MyContosoUniversity.Web.Data.SchoolContext _context;

        public IndexModel(MyContosoUniversity.Web.Data.SchoolContext context)
        {
            _context = context;
        }

        public IList<Instructor> Instructor { get;set; } = default!;

        public InstructorIndexViewModel InstructorViewModel { get; set; }
        public int InstructorID { get; set; }
        public int CourseID { get; set; }

        public async Task OnGetAsync(int? id, int? courseID)
        {
            InstructorViewModel = new InstructorIndexViewModel();
            InstructorViewModel.Instructors = await _context.Instructors
                                                        .Include(i => i.OfficeAssignment)
                                                        .Include(i => i.Courses)
                                                        .ThenInclude(c => c.Department)
                                                        .OrderBy(i => i.LastName)
                                                        .ToListAsync();

            if (id.HasValue)
            {
                InstructorID = id.Value;

                InstructorViewModel.Courses = InstructorViewModel
                                                .Instructors
                                                .Single(i => i.ID == id.Value)
                                                .Courses;
            }

            if (courseID.HasValue)
            {
                CourseID = courseID.Value;

                var selectedCourse = InstructorViewModel
                                        .Courses
                                        .Single(x => x.CourseID == courseID);

                // TODO Why is this Entry.Collection method used???
                await _context.Entry(selectedCourse)
                              .Collection(x => x.Enrollments)
                              .LoadAsync();

                foreach (var enrollment in selectedCourse.Enrollments)
                {
                    // TODO Why is this Entry.Reference method used???
                    await _context.Entry(enrollment)
                                  .Reference(x => x.Student)
                                  .LoadAsync();
                }

                InstructorViewModel.Enrollments = selectedCourse.Enrollments;
            }
        }
    }
}
