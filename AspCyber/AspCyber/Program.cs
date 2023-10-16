using AspCyber.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false).AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
        builder.Services.AddRazorPages();

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
            var roleMeneger = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var roles = new[] { "Admin", "User" };

            foreach (var role in roles)
            {
                if (!await roleMeneger.RoleExistsAsync(role))
                    await roleMeneger.CreateAsync(new IdentityRole(role));
            }
        }

        app.MapRazorPages();
        using (var scope = app.Services.CreateScope())
        {
            var userMeneger = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            string email = "admin@admin.com";
            string password = "Haslo123!";

            if(await userMeneger.FindByEmailAsync(email) == null)
            {
                var user = new IdentityUser();
                user.Email = email;
                user.UserName = email;

                await userMeneger.CreateAsync(user, password);

                await userMeneger.AddToRoleAsync(user, "Admin");
            }
        }
        app.Run();
    }
}