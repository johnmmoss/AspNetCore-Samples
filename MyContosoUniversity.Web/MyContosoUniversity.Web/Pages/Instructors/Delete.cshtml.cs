using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyContosoUniversity.Web.Data;
using MyContosoUniversity.Web.Models;

namespace MyContosoUniversity.Web.Pages.Instructors
{
    public class DeleteModel : PageModel
    {
        private readonly MyContosoUniversity.Web.Data.SchoolContext _context;

        public DeleteModel(MyContosoUniversity.Web.Data.SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Instructor Instructor { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Instructors == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructors.FirstOrDefaultAsync(m => m.ID == id);

            if (instructor == null)
            {
                return NotFound();
            }
            else 
            {
                Instructor = instructor;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Instructors == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructors
                .Include(i => i.Courses)
                .SingleAsync(i => i.ID == id);

            if (instructor == null)
            {
                return RedirectToPage("./Index");
            }

            // An Instructor has:
            // - Courses
            // - OfficeAssignments
            // - Departments

            // The first two of these are set to Cascade Delete.
            // Departments is not, so needs to be deleted manually.

            // NOTE Docs say that:
            // "Courses must be included or they aren't deleted when the instructor is deleted."
            // But they ARE actually cascade delete in the db ???

            var departments = await _context.Departments
                                                .Where(d => d.InstructorID == id)
                                                .ToListAsync();

            departments.ForEach(d => d.InstructorID = null);

            _context.Instructors.Remove(instructor);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
