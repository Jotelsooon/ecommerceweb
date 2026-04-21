using ECommerceWeb.DataAccess;
using ECommerceWeb.DataAccess.Repository;
using ECommerceWeb.Models;
using ECommerceWeb.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Stripe;
using ECommerceWeb;

var builder = WebApplication.CreateBuilder(args);

// Base de datos
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration
        .GetConnectionString("DefaultConnection")));

// Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => {
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Stripe
builder.Services.Configure<StripeSettings>(
    builder.Configuration.GetSection("Stripe"));

// Dependencias
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailSender, EmailSender>();

// MVC
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.ConfigureApplicationCookie(options => {
    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});

var app = builder.Build();

// Stripe key global
StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

// Seed de roles
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider
        .GetRequiredService<RoleManager<IdentityRole>>();
    string[] roles = { SD.Role_Customer, SD.Role_Admin, SD.Role_Employee };
    foreach (var role in roles)
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
