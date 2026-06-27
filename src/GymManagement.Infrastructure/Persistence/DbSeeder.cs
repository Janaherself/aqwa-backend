using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Entities;
using GymManagement.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GymManagement.Infrastructure.Persistence;

public static class DbSeeder
{
    public static async Task SeedAsync(
        GymDbContext context,
        IPasswordHasher hasher,
        IConfiguration config,
        ILogger logger)
    {
        await context.Database.MigrateAsync();

        if (await context.Gyms.AnyAsync())
        {
            logger.LogInformation("Database already seeded — skipping.");
            return;
        }

        var gymName     = config["Seeding:GymName"]      ?? "My Gym";
        var username    = config["Seeding:AdminUsername"] ?? "admin";
        var password    = config["Seeding:AdminPassword"] ?? "Admin@123";
        var phone       = config["Seeding:AdminPhone"]    ?? "0000000000";

        var gym       = Gym.Create(gymName);
        var adminRole = Role.Create(gym.Id, "Admin", Permission.Admin);
        var adminUser = User.Create(gym.Id, username, hasher.Hash(password), phone, adminRole.Id);

        context.Add(gym);
        context.Add(adminRole);
        context.Add(adminUser);
        await context.SaveChangesAsync();

        logger.LogInformation(
            "Database seeded. Gym: '{GymName}' | Admin username: '{Username}'",
            gymName, username);
    }
}
