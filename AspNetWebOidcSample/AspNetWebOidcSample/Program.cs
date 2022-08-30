using AspNetWebOidcSample.Security;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = AuthenticationSettings.CookieAuthenticationScheme;
    options.DefaultChallengeScheme = AuthenticationSettings.OpenIdConnectAuthenticationScheme;
}).AddCookie(AuthenticationSettings.CookieAuthenticationScheme, options =>
{
    options.Cookie.SameSite = SameSiteMode.None;
}).AddOpenIdConnect(AuthenticationSettings.OpenIdConnectAuthenticationScheme, options =>
{
    options.Authority = "http://localhost:8080/realms/master";
    options.ClientId = "WebClient1";
    options.ClientSecret = "MzqLDzjFkoiRbdQTiAd27TQYe5jCHHfE";
    options.RequireHttpsMetadata = false;
    options.ResponseType = OpenIdConnectResponseType.Code;
    options.SaveTokens = true;
    options.UseTokenLifetime = true;
    options.CorrelationCookie.SameSite = SameSiteMode.None;
    options.NonceCookie.SameSite = SameSiteMode.None;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true
    };
    // options.Events.OnTokenValidated = TokenValidationHandler.OnTokenValidatedHandler;
});
builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
});


app.Run();
