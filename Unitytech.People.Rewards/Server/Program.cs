using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Unitytech.People.Rewards.Data.Repository;
using Unitytech.People.Rewards.Server.Data;
using Unitytech.People.Rewards.Server.Models;


var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

var dbConnectionString = builder.Configuration.GetConnectionString("DBConnection");
builder.Services.AddDbContext<DatabaseContext>((options) =>
{
    options.UseSqlServer(connectionString, p => p.MigrationsAssembly("Unitytech.People.Rewards.Server"));
});
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyHeader();
                          policy.AllowAnyMethod();
                          policy.WithOrigins($"http://rewards.leonvandebroek.nl",
                                $"https://rewards.leonvandebroek.nl");
                      });
});


var identityBuilder = builder.Services.AddIdentityServer((options) =>
{
    options.IssuerUri = "https://rewards.leonvandebroek.nl";
});
identityBuilder
    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>(options =>
    {
        options.IdentityResources["openid"].UserClaims.Add("role"); // Roles
        options.ApiResources.Single().UserClaims.Add("role");
        options.IdentityResources["openid"].UserClaims.Add("custom_claim"); // Custom Claim
        options.ApiResources.Single().UserClaims.Add("custom_claim");
        options.IdentityResources["openid"].UserClaims.Add("custom_claim2"); // Custom Claim
        options.ApiResources.Single().UserClaims.Add("custom_claim2");

        options.IdentityResources["openid"].UserClaims.Add("Application.Permission"); // Custom Claim
        options.ApiResources.Single().UserClaims.Add("Application.Permission");


    });

identityBuilder.AddDeveloperSigningCredential();

builder.Services.AddAuthentication()
    .AddIdentityServerJwt()
    .AddCookie(options =>
    {
        // add an instance of the patched manager to the options:
        options.CookieManager = new ChunkingCookieManager();
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    });


builder.Services.AddControllersWithViews().AddJsonOptions(j =>
{
    j.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});
builder.Services.AddRazorPages();

var app = builder.Build();


using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetService<DatabaseContext>();
    context?.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();
var forwardOptions = new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
    RequireHeaderSymmetry = false
};

forwardOptions.KnownNetworks.Clear();
forwardOptions.KnownProxies.Clear();
app.UseForwardedHeaders(forwardOptions);
app.UseCors(MyAllowSpecificOrigins);
app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();