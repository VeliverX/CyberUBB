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

        builder.Services.Configure<IdentityOptions>(options =>
        {
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(20);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;
        });

        builder.Services.Configure<IdentityOptions>(options =>
            {
                //Password settings.
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
            var roleMeneger = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var roles = new[] { "Admin", "User", "Guest", "Moderator", "Owner" };

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
            //Admin
            string emailAdmin = "admin@admin.com";
            string passwordAdmin = "Haslo123!";
            //User
            string emailUser = "user@user.com";
            string passwordUser = "Haslo123!";

            string emailUser2 = "user2@user.com";
            string passwordUser2 = "Haslo123!";

            string emailUser3 = "user3@user.com";
            string passwordUser3 = "Haslo123!";

            string emailUser4 = "user4@user.com";
            string passwordUser4 = "Haslo123!";

            if (await userMeneger.FindByEmailAsync(emailAdmin) == null)
            {
                var user = new IdentityUser();
                user.Email = emailAdmin;
                user.UserName = emailAdmin;

                await userMeneger.CreateAsync(user, passwordAdmin);

                await userMeneger.AddToRoleAsync(user, "Admin");
            }
            if (await userMeneger.FindByEmailAsync(emailUser) == null)
            {
                var user = new IdentityUser();
                user.Email = emailUser;
                user.UserName = emailUser;

                await userMeneger.CreateAsync(user, passwordUser);

                await userMeneger.AddToRoleAsync(user, "User");
            }
            if (await userMeneger.FindByEmailAsync(emailUser2) == null)
            {
                var user = new IdentityUser();
                user.Email = emailUser2;
                user.UserName = emailUser2;

                await userMeneger.CreateAsync(user, passwordUser2);

                await userMeneger.AddToRoleAsync(user, "User");
            }
            if (await userMeneger.FindByEmailAsync(emailUser3) == null)
            {
                var user = new IdentityUser();
                user.Email = emailUser3;
                user.UserName = emailUser3;

                await userMeneger.CreateAsync(user, passwordUser3);

                await userMeneger.AddToRoleAsync(user, "User");
            }
            if (await userMeneger.FindByEmailAsync(emailUser4) == null)
            {
                var user = new IdentityUser();
                user.Email = emailUser4;
                user.UserName = emailUser4;

                await userMeneger.CreateAsync(user, passwordUser4);

                await userMeneger.AddToRoleAsync(user, "User");
            }
        }
        app.Run();
    }
}