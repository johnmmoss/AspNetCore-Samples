using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyContosoUniversity.Web.Data;
using MyContosoUniversity.Web.Models;

namespace MyContosoUniversity.Web.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly MyContosoUniversity.Web.Data.SchoolContext _context;
        private readonly IConfiguration _configuration;

        public IndexModel(MyContosoUniversity.Web.Data.SchoolContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<Student> Students { get;set; }

        public async Task OnGetAsync(string sortOrder,
            string currentFilter, 
            string searchString, 
            int? pageIndex)
        {
            CurrentSort = sortOrder;
            // This code is not very clear. By default there is no sortOrder specified
            // and the default switch clause statement will sort by last name ascending.
            // So  there is no explicit "Name" (that is the equivalent of name_asc) 
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            IQueryable<Student> studentsIQ = from s in _context.Students
                                             select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                studentsIQ = studentsIQ.Where(s => s.LastName.Contains(searchString)
                                                   || s.FirstMidName.Contains(searchString));
            }

            // You can use dynamic linq to simplfy this for large numbers of options
            // see: https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/advanced?view=aspnetcore-6.0#dynamic-linq
            // Uses: students.OrderBy(e => EF.Property<object>(e, sortOrder))

            switch (sortOrder)
            {
                case "name_desc":
                    studentsIQ = studentsIQ.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    studentsIQ = studentsIQ.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    studentsIQ = studentsIQ.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:
                    studentsIQ = studentsIQ.OrderBy(s => s.LastName);
                    break;
            }

            var pageSize = _configuration.GetValue("PageSize", 4);
            Students = await PaginatedList<Student>.CreateAsync(studentsIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
