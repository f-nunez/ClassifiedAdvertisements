using System.Security.Claims;
using Identity.Domain.Entities;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Identity.Infrastructure.Persistence.Contexts;

public class ApplicationDbContextSeeder
{
    private const string DemoPassword = "P4$$w0rd";
    private readonly ILogger<ApplicationDbContextSeeder> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public ApplicationDbContextSeeder(
        ILogger<ApplicationDbContextSeeder> logger,
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
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
        if (!await _context.Users.AnyAsync())
        {
            var managerUserDemo = new ApplicationUser
            {
                Id = "3541fa79-0c35-4626-8dc5-7ac4414eed04",
                Name = "Francisco Nuñez",
                Email = "francisco@nunez.ninja",
                UserName = "francisco"
            };

            var staffUserDemo = new ApplicationUser
            {
                Id = "2abe8c75-51f7-489a-a87b-c4867ef1e220",
                Name = "Ramon Nuñez",
                Email = "ramon@nunez.ninja",
                UserName = "ramon"
            };

            var customerUserDemo = new ApplicationUser
            {
                Id = "162bb879-33a8-4a25-b11d-8fa14d8e87ed",
                Name = "Chris Nuñez",
                Email = "chris@nunez.ninja",
                UserName = "chris"
            };

            await _userManager.CreateAsync(managerUserDemo, DemoPassword);

            await _userManager.CreateAsync(staffUserDemo, DemoPassword);

            await _userManager.CreateAsync(customerUserDemo, DemoPassword);

            await _userManager.AddClaimsAsync(managerUserDemo, new Claim[]{
                new(JwtClaimTypes.Name, "Francisco Nuñez")
            });

            await _userManager.AddClaimsAsync(staffUserDemo, new Claim[]{
                new(JwtClaimTypes.Name, "Ramon Nuñez")
            });

            await _userManager.AddClaimsAsync(customerUserDemo, new Claim[]{
                new(JwtClaimTypes.Name, "Chris Nuñez")
            });
        }

        if (!await _context.Roles.AnyAsync())
        {
            await _context.AddRangeAsync(
                new IdentityRole
                {
                    Id = "089cb161-fce4-4d55-acc5-a40603a385ba",
                    Name = "Manager"
                },
                new IdentityRole
                {
                    Id = "1f4d522f-429e-4e14-9a25-0a38cf6a587e",
                    Name = "Staff"
                },
                new IdentityRole
                {
                    Id = "98253fe9-5912-4c92-93c3-54035d204be8",
                    Name = "Customer"
                }
            );

            await _context.SaveChangesAsync();
        }

        if (!await _context.UserRoles.AnyAsync())
        {
            await _context.UserRoles.AddRangeAsync(
                new IdentityUserRole<string>
                {
                    RoleId = "089cb161-fce4-4d55-acc5-a40603a385ba",
                    UserId = "3541fa79-0c35-4626-8dc5-7ac4414eed04"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "1f4d522f-429e-4e14-9a25-0a38cf6a587e",
                    UserId = "2abe8c75-51f7-489a-a87b-c4867ef1e220"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "98253fe9-5912-4c92-93c3-54035d204be8",
                    UserId = "162bb879-33a8-4a25-b11d-8fa14d8e87ed"
                }
            );

            await _context.SaveChangesAsync();
        }
    }
}