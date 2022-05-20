using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyContosoUniversity.Web.Data;
using MyContosoUniversity.Web.Models;

namespace MyContosoUniversity.Web.Pages.Courses
{
    public class CreateModel : DepartmentNamePageModel 
    {
        private readonly MyContosoUniversity.Web.Data.SchoolContext _context;

        public CreateModel(MyContosoUniversity.Web.Data.SchoolContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["DepartmentID"] = new SelectList(_context.Departments, "DepartmentID", "DepartmentID");

            PopulateDepartmentsDropDownList(_context);

            return Page();
        }

        [BindProperty]
        public Course Course { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var emptyCourse = new Course();

            if (await TryUpdateModelAsync<Course>(
                    emptyCourse,
                    "course", // Prefix for form value.
                    s => s.CourseID,
                    s => s.DepartmentID,
                    s => s.Title,
                    s => s.Credits))
            {
                _context.Courses.Add(emptyCourse);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }

            // Rebuild SelectList ready to reshow page (if an error)
            PopulateDepartmentsDropDownList(_context, emptyCourse.DepartmentID);

            return Page();
        }
    }
}
