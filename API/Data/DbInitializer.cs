using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public static class DbInitializer
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.Users.AnyAsync()) return;

        var users = new List<AppUser>
        {
            new AppUser { DisplayName = "Aman", Email = "aman@test.com" },
            new AppUser { DisplayName = "Rahul", Email = "rahul@test.com" },
            new AppUser { DisplayName = "Neha", Email = "neha@test.com" },
            new AppUser { DisplayName = "Test User", Email = "test@test.com" }
        };

        context.Users.AddRange(users);
        await context.SaveChangesAsync();
    }
}
