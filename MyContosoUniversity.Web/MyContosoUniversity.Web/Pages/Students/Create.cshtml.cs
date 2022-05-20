using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyContosoUniversity.Web.Data;
using MyContosoUniversity.Web.Models;

namespace MyContosoUniversity.Web.Pages.Students
{
    public class CreateModel : PageModel
    {
        private readonly MyContosoUniversity.Web.Data.SchoolContext _context;

        public CreateModel(MyContosoUniversity.Web.Data.SchoolContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Student Student { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Susceptible to overposting:
            // See: https://docs.microsoft.com/en-us/aspnet/core/data/ef-rp/crud?view=aspnetcore-6.0#overposting
            //_context.Students.Add(Student);
            //await _context.SaveChangesAsync();

            var emptyStudent = new Student();

            // Use TryUpdateModel instead:
            if (await TryUpdateModelAsync<Student>(
                    emptyStudent,
                    "student",   // Prefix for form value.
                    s => s.FirstMidName, 
                    s => s.LastName, 
                    s => s.EnrollmentDate))
            {
                _context.Students.Add(emptyStudent);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return RedirectToPage("./Index");
        }
    }
}
