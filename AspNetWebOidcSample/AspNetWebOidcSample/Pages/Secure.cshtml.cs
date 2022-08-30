using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNetWebOidcSample.Pages;

[Authorize]
public class Secure : PageModel
{
    private IHttpContextAccessor _httpContextAccessor;

    public Secure(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task OnGet()
    {
        // GetTokenAsync extension method is in the package Microsoft.AspNetCore.Authentication
        var token = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");


        var user = _httpContextAccessor.HttpContext.User;

    }
}