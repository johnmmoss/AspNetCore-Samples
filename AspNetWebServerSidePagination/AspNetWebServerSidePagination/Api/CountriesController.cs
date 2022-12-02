using AspNetWebServerSidePagination.Models;
using CsvHelper;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace AspNetWebServerSidePagination.Api
{
    [ApiController]
    public class CountriesController : ControllerBase
    {
        [HttpGet]
        [Route("/filter")]
        public async Task<IActionResult> Get(IDataTablesRequest request)
        {
            var searchTerm = request.Search.Value;
            var sortColumn = request.Columns.FirstOrDefault(x => x.Sort != null);
            var sortField = sortColumn != null ? sortColumn.Field : "name";
            var sortDirection = sortColumn != null ? sortColumn.Sort.Direction : SortDirection.Ascending;

            var countries = GetCountries();

            var searchedCountries = string.IsNullOrEmpty(searchTerm)
                ? countries
                : countries.Where(x => x.Name.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase) ||
                                       x.Capital.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase) ||
                                       x.Continent.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase)).ToList();

            IEnumerable<Country> sortedAndSearchedCountries;

            switch (sortField)
            {
                case "capital":
                    sortedAndSearchedCountries = sortDirection == SortDirection.Ascending
                        ? searchedCountries.OrderBy(x => x.Capital)
                        : searchedCountries.OrderByDescending(x => x.Capital).ToList();
                    break;
                case "continent":
                    sortedAndSearchedCountries = sortDirection == SortDirection.Ascending
                        ? searchedCountries.OrderBy(x => x.Continent)
                        : searchedCountries.OrderByDescending(x => x.Continent);
                    break;
                default:
                    sortedAndSearchedCountries = sortDirection == SortDirection.Ascending
                        ? searchedCountries.OrderBy(x => x.Name)
                        : searchedCountries.OrderByDescending(x => x.Name);
                    break;

            }

            var pagedGridData = sortedAndSearchedCountries
                                    .Skip(request.Start)
                                    .Take(request.Length)
                                    .ToList();

            var totalCount = countries.Count();
            var totalRecordsFiltered = searchedCountries.Count();
            var response = DataTablesResponse.Create(request, totalCount, totalRecordsFiltered, pagedGridData);

           return new DataTablesJsonResult(response, true);
        }

        private List<Country> GetCountries()
        {
            var path = @"Data\countries.csv";
            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            return csv.GetRecords<Country>().ToList();
        }
    }
}
