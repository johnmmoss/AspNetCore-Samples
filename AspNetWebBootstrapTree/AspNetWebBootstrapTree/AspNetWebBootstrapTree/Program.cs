using AspNetWebBootstrapTree.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
var config = builder.Configuration.GetSection(GoogleApiSettings.Name);
builder.Services.Configure<GoogleApiSettings>(builder.Configuration.GetSection(GoogleApiSettings.Name));


var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.UseEndpoints(endpoints => { endpoints.MapRazorPages(); });

app.Run();