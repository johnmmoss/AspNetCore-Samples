namespace AspNetWebBootstrapTree.Models;

public class CountryTreeViewModel
{
    public string Name { get; set; }

    public List<CountyTreeViewModel> Counties { get; set; }
}