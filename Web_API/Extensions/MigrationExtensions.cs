using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Web_API.Extensions;

public static class MigrationExtensions
{
    public static void InitialMigration(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        serviceScope.ServiceProvider.GetService<MotelManagement2024DbContext>().Database.Migrate();
    }
}
