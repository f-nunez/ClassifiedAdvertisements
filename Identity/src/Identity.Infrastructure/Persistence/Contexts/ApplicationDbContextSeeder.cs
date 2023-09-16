using System.Security.Claims;
using Identity.Domain.Entities;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Identity.Infrastructure.Persistence.Contexts;

public class ApplicationDbContextSeeder
{
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
}