using Hope_for_Organ_Donation.Data;
using Hope_for_Organ_Donation.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Services
        builder.Services.AddControllers().AddNewtonsoftJson();
        builder.Services.AddDbContext<AppDbContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddIdentityCore<AppUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "Hope for Organ Donation API",
                Version = "v1"
            });
        });

        var app = builder.Build();

        // Middleware
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hope for Organ Donation API v1");
            });
        }
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            SeedRoles(roleManager).Wait();
        }


            async Task SeedRoles(RoleManager<IdentityRole> roleManager)
            {
                string[] roles = new[] { "Admin", "Hospital", "Donor" };

                foreach (var role in roles)
                {
                    var exists = await roleManager.RoleExistsAsync(role);
                    if (!exists)
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
            }
        
            app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}