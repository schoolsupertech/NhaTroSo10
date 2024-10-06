using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;

namespace Web_API.Extensions;

public static class MigrationExtensions
{
    public static void UseDefaultMigrations(this IApplicationBuilder app)
    {
        using IServiceScope iServiceScope = app.ApplicationServices.CreateScope();
        var serviceDbContext = iServiceScope.ServiceProvider.GetRequiredService<MotelManagement2024DbContext>();

        if (serviceDbContext.Database.GetPendingMigrations().Any())
        {
            serviceDbContext.Database.Migrate();
        }
    }
}
