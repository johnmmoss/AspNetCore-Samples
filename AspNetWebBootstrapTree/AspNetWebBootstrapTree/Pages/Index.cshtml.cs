using AspNetWebBootstrapTree.Configuration;
using AspNetWebBootstrapTree.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace AspNetWebBootstrapTree.Pages;

public class IndexModel : PageModel
{
    public IndexModel(IOptions<GoogleApiSettings> settings)
    {
        if (settings.Value.EmbeddedMapsApiKey == "NOTSET")
            throw new ApplicationException(
                "Register an API_KEY for the Embedded Maps API at https://console.cloud.google.com and add to the EmbeddedMapsApiKey setting.");

        EmbeddedMapsApiKey = settings.Value.EmbeddedMapsApiKey;
    }

    public TreeViewModel TreeModel { get; set; }
    public string EmbeddedMapsApiKey { get; set; }

    public void OnGet()
    {
        var parsedCityRows = System.IO.File.ReadAllLines("data\\Towns.csv")
            .Where(x => x.Split(",")[0] != "Town")
            .Select(x =>
            {
                var parsedRow = x.Split(",");
                return new CityRow
                {
                    City = parsedRow[0],
                    County = parsedRow[1],
                    Country = parsedRow[2]
                };
            })
            .ToList();

        var groupedCityRows = parsedCityRows
            .GroupBy(
                city => new { city.Country, city.County },
                city => city,
                (key, grouping) =>
                {
                    return new GroupedCityRow
                    {
                        Country = key.Country,
                        County = key.County,
                        Cities = grouping.Select(x => x.City).ToList()
                    };
                })
            .ToList();

        var englandCounties = FilterToCountyTreeViewModel(groupedCityRows, "England");
        var walesCounties = FilterToCountyTreeViewModel(groupedCityRows, "Wales");
        var scotlandCounties = FilterToCountyTreeViewModel(groupedCityRows, "Scotland");
        var northernIrelandCounties = FilterToCountyTreeViewModel(groupedCityRows, "Northern Ireland");

        var treeViewModel = new TreeViewModel
        {
            Countries = new List<CountryTreeViewModel>
            {
                new() { Name = "England", Counties = englandCounties },
                new() { Name = "Wales", Counties = walesCounties },
                new() { Name = "Scotland", Counties = scotlandCounties },
                new() { Name = "Northern Ireland", Counties = northernIrelandCounties }
            }
        };

        TreeModel = treeViewModel;
    }

    public List<CountyTreeViewModel> FilterToCountyTreeViewModel(
        List<GroupedCityRow> groupedCityRows,
        string countryName)
    {
        return groupedCityRows
            .Where(x => x.Country.ToLower() == countryName.ToLower())
            .Select(x => new CountyTreeViewModel { Name = x.County, Cities = x.Cities })
            .ToList();
    }
}