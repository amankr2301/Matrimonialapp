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
            new AppUser
            {
                DisplayName = "Aman",
                Email = "aman@test.com",
                passwordHash = new byte[] { 1, 2, 3, 4, 5 },
                passwordSalt = new byte[] { 10, 20, 30, 40, 50 }
            },
            new AppUser
            {
                DisplayName = "Rahul",
                Email = "rahul@test.com",
                // SAME password (hash + salt) as Aman
                passwordHash = new byte[] { 1, 2, 3, 4, 5 },
                passwordSalt = new byte[] { 10, 20, 30, 40, 50 }
            },
            new AppUser
            {
                DisplayName = "Neha",
                Email = "neha@test.com",
                passwordHash = new byte[] { 6, 7, 8, 9, 10 },
                passwordSalt = new byte[] { 60, 70, 80, 90, 100 }
            },
            new AppUser
            {
                DisplayName = "Test User",
                Email = "test@test.com",
                passwordHash = new byte[] { 11, 12, 13, 14, 15 },
                passwordSalt = new byte[] { 110, 120, 130, 140, 150 }
            }
        };

        context.Users.AddRange(users);
        await context.SaveChangesAsync();
    }
}
