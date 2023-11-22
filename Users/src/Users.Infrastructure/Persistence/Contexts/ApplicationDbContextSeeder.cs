using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Users.Domain.Entities;

namespace Users.Infrastructure.Persistence.Contexts;

public class ApplicationDbContextSeeder
{
    private readonly ILogger<ApplicationDbContextSeeder> _logger;
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextSeeder(
        ILogger<ApplicationDbContextSeeder> logger,
        ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task MigrateAsync()
    {
        try
        {
            if (_context.Database.IsNpgsql())
                await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedDataAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    private async Task TrySeedAsync()
    {
        if (!await _context.Roles.AnyAsync())
        {
            await _context.Roles.AddRangeAsync(GetRoles());
            await _context.SaveChangesAsync();
        }

        if (!await _context.Users.AnyAsync())
        {
            await _context.Users.AddRangeAsync(GetUsers());
            await _context.SaveChangesAsync();
        }

        if (!await _context.UserRoles.AnyAsync())
        {
            await _context.UserRoles.AddRangeAsync(GetUserRoles());
            await _context.SaveChangesAsync();
        }
    }

    private List<Role> GetRoles()
    {
        return new List<Role>
        {
            new()
            {
                Id = "98253fe9-5912-4c92-93c3-54035d204be8",
                IsActive = true,
                Name = "Customer",
                NormalizedName = "Customer".Normalize().ToUpper()
            },
            new() {
                Id = "089cb161-fce4-4d55-acc5-a40603a385ba",
                IsActive = true,
                Name = "Manager",
                NormalizedName = "Manager".Normalize().ToUpper()
            },
            new() {
                Id = "1f4d522f-429e-4e14-9a25-0a38cf6a587e",
                IsActive = true,
                Name = "Staff",
                NormalizedName = "Staff".Normalize().ToUpper()
            }
        };
    }

    private List<User> GetUsers()
    {
        return new List<User>
        {
            new()
            {
                Email = "francisco@nunez.ninja",
                FirstName = "Francisco",
                FullName ="Francisco Nuñez",
                Id = "3541fa79-0c35-4626-8dc5-7ac4414eed04",
                IsActive = true,
                LastName = "Nuñez",
                NormalizedEmail = "francisco@nunez.ninja".Normalize().ToUpper(),
                NormalizedFirstName = "Francisco".Normalize().ToUpper(),
                NormalizedFullName = "Francisco Nuñez".Normalize().ToUpper(),
                NormalizedLastName = "Nuñez".Normalize().ToUpper(),
                NormalizedUserName = "francisco".Normalize().ToUpper(),
                PhoneNumber = "+521111111111",
                ProfileImageUrl = null,
                UserName = "francisco"
            },
            new()
            {
                Email = "ramon@nunez.ninja",
                FirstName = "Ramon",
                FullName ="Ramon Nuñez",
                Id = "2abe8c75-51f7-489a-a87b-c4867ef1e220",
                IsActive = true,
                LastName = "Nuñez",
                NormalizedEmail = "ramon@nunez.ninja".Normalize().ToUpper(),
                NormalizedFirstName = "Ramon".Normalize().ToUpper(),
                NormalizedFullName = "Ramon Nuñez".Normalize().ToUpper(),
                NormalizedLastName = "Nuñez".Normalize().ToUpper(),
                NormalizedUserName = "ramon".Normalize().ToUpper(),
                PhoneNumber = "+521111111112",
                ProfileImageUrl = null,
                UserName = "ramon"
            },
            new()
            {
                Email = "chris@nunez.ninja",
                FirstName = "Chris",
                FullName ="Chris Nuñez",
                Id = "162bb879-33a8-4a25-b11d-8fa14d8e87ed",
                IsActive = true,
                LastName = "Nuñez",
                NormalizedEmail = "chris@nunez.ninja".Normalize().ToUpper(),
                NormalizedFirstName = "Chris".Normalize().ToUpper(),
                NormalizedFullName = "Chris Nuñez".Normalize().ToUpper(),
                NormalizedLastName = "Nuñez".Normalize().ToUpper(),
                NormalizedUserName = "chris".Normalize().ToUpper(),
                PhoneNumber = "+521111111113",
                ProfileImageUrl = null,
                UserName = "chris"
            }
        };
    }

    private List<UserRole> GetUserRoles()
    {
        return new List<UserRole>
        {
            new()
            {
                Id = "b72902d3-25d4-4e70-afa1-1baed255dc3c",
                IsActive = true,
                RoleId = "089cb161-fce4-4d55-acc5-a40603a385ba",
                UserId = "3541fa79-0c35-4626-8dc5-7ac4414eed04"
            },
            new()
            {
                Id = "d31dc2c2-6cdd-437c-b6fc-63398ed5f1ef",
                IsActive = true,
                RoleId = "1f4d522f-429e-4e14-9a25-0a38cf6a587e",
                UserId = "2abe8c75-51f7-489a-a87b-c4867ef1e220"
            },
            new()
            {
                Id = "a1165810-8192-487a-8bbf-743f7d1071b2",
                IsActive = true,
                RoleId = "98253fe9-5912-4c92-93c3-54035d204be8",
                UserId = "162bb879-33a8-4a25-b11d-8fa14d8e87ed"
            }
        };
    }
}