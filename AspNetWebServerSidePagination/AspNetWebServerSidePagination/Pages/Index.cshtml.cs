using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
using CsvHelper;
using AspNetWebServerSidePagination.Models;

namespace AspNetWebServerSidePagination.Pages
{
    public class IndexModel : PageModel
    {
        public IEnumerable<Country> Countries { get; set; }
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            //var path = @"Data\countries.csv";
            //using (var reader = new StreamReader(path))
            //using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            //{
            //    Countries = csv.GetRecords<Country>().ToList();
            //}
        }
    }
}