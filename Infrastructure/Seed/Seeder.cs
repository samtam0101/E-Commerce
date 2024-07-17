using Domain.Constants;
using Domain.DTOs.RolePermissionDto;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Helpers;
using Infrastructure.Services.HashService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Seed;

public class Seeder
{
    private readonly ILogger<Seeder> logger;
    private readonly IHashService hashService;
    private readonly DataContext context;
    public Seeder(IHashService hashService, DataContext context, ILogger<Seeder> logger)
    {
        this.logger = logger;
        this.context = context;
        this.hashService = hashService;
    }
    public async Task Initial()
    {
        await SeedRoles();
        await DefaultUsers();
    }
    private async Task SeedRoles()
    {
        try
        {
            logger.LogInformation("SeedRoles method started at {DateTime}", DateTime.UtcNow);
            var newRoles = new List<Role>()
            {
                new Role()
                {
                    Id = 1,
                    Name = Roles.Admin
                },
                new Role()
                {
                    Id = 2,
                    Name = Roles.User
                }
            };
            var existingRoles = await context.Roles.ToListAsync();
            foreach (var item in newRoles)
            {
                if (existingRoles.Exists(x => x.Name == item.Name) == false)
                {
                    await context.Roles.AddAsync(item);
                }
            }
            await context.SaveChangesAsync();
            logger.LogInformation("SeedRoles method finished at {DateTime}", DateTime.UtcNow);
            return;
        }
        catch (Exception e)
        {
            logger.LogError("Exception {EMessage}, time: {DateTimeNow}", e.Message, DateTime.UtcNow);
        }
    }
    private async Task DefaultUsers()
    {
        //Admin
        logger.LogInformation("Method DefaultUsers started time: {dateTime}", DateTime.UtcNow);
        var existingAdmin = await context.Users.FirstOrDefaultAsync(x => x.FirstName == "Admin");
        if (existingAdmin == null)
        {
            var admin = new User()
            {
                Id = 1,
                FirstName = "Admin",
                LastName = "Adminov",
                Email = "admin@gmail.com",
                PasswordHash = hashService.ConvertToHash("12345"),
                RegistrationDate = DateTime.UtcNow
            };
            await context.Users.AddAsync(admin);
            await context.SaveChangesAsync();
            var existingUser = await context.Users.FirstOrDefaultAsync(x => x.FirstName == "Admin" && x.LastName == "Adminov");
            var existingRole = await context.Roles.FirstOrDefaultAsync(x => x.Name == Roles.Admin);
            if (existingUser != null && existingRole != null)
            {
                var userRole = new UserRole()
                {
                    UserId = existingUser.Id,
                    RoleId = existingRole.Id,
                    Role = existingRole,
                    User = existingUser
                };
                await context.UserRoles.AddAsync(userRole);
                await context.SaveChangesAsync();
            }
        }
        await PermissionsForAdmin();


        var existingUser2 = await context.Users.FirstOrDefaultAsync(x => x.FirstName == "User" && x.LastName == "Userov");
        if ( existingUser2 == null)
        {
            var user2 = new User()
            {
                Id = 2,
                FirstName = "User",
                LastName = "Userov",
                Email = "user@gmail.com",
                PasswordHash = hashService.ConvertToHash("12345"),
                RegistrationDate = DateTime.UtcNow
            };
            await context.Users.AddAsync(user2);
            await context.SaveChangesAsync();

            var existingUser = await context.Users.FirstOrDefaultAsync(x => x.FirstName == "User" && x.LastName == "Userov");
            var existingRole = await context.Roles.FirstOrDefaultAsync(x => x.Name == Roles.User);
            if (existingUser is not null && existingRole is not null)
            {
                var userRole = new UserRole()
                {
                    RoleId = existingRole.Id,
                    UserId = existingUser.Id,
                    Role = existingRole,
                    User = existingUser
                };
                await context.UserRoles.AddAsync(userRole);
                await context.SaveChangesAsync();
            } 
        }
        await PermissionsForUser();

    }
    private async Task PermissionsForAdmin()
    {
        try
        {
            var adminRole = await context.Roles.FirstOrDefaultAsync(x => x.Name == Roles.Admin);
            if (adminRole == null) return;
            var roleClaims = new List<RoleClaimsDto>();
            roleClaims.GetPermissions(typeof(Domain.Constants.Permissions));
            var existingClaims = await context.RoleClaims.Where(x => x.RoleId == adminRole.Id).ToListAsync();
            foreach (var claim in roleClaims)
            {
                if (existingClaims.Any(c => c.ClaimType == claim.Value) == false)
                    await context.AddPermissionClaim(adminRole, claim.Value);
            }
        }
        catch (Exception ex)
        {
            // ignored
        }
    }
    private async Task PermissionsForUser()
    {
        var userRole = await context.Roles.FirstOrDefaultAsync(x => x.Name == Roles.User);
        if (userRole == null) return;
        var userClaims = new List<RoleClaimsDto>()
        {
            new ("Permissions", Domain.Constants.Permissions.Products.View),
            new ("Permissions", Domain.Constants.Permissions.Categories.View)
        };
    }
}