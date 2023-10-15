using ASP_CB.Areas.Admin.Models;
using ASP_CB.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>().
                AddDefaultUI().AddDefaultTokenProviders();

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Default Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 1;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{

    var _userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
    var _roleManager = scope.ServiceProvider.GetService<RoleManager<ApplicationRole>>();

    var createAdminRole = await _roleManager.CreateAsync(
        new ApplicationRole()
        {
            Name = "Administrator"
        });

    var createUserRole = await _roleManager.CreateAsync(
        new ApplicationRole()
        {
            Name = "User"
        });


    var createResult = await _userManager.CreateAsync(
    new ApplicationUser()
    {
        UserName = "admin@admin.com",
        Email = "admin@admin.com",
        LockoutEnabled = false,
        AccessFailedCount = 0,



    }, "Haslo123!");

    var createUser = await _userManager.CreateAsync(
    new ApplicationUser()
    {
        UserName = "nikodem@ubb.edu",
        Email = "nikodem@ubb.edu",
        LockoutEnabled = false,
        AccessFailedCount = 0,



    }, "Haslo123!");

    var adminUser =  _userManager.FindByNameAsync("admin@admin.com").Result;
    var User = _userManager.FindByNameAsync("nikodem@ubb.edu").Result;
    _userManager.AddToRoleAsync(adminUser, "Administrator");
    _userManager.AddToRoleAsync(User, "User");
}

    app.Run();
