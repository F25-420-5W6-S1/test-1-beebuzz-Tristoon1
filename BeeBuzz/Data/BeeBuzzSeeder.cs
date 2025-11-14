using BeeBuzz.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace BeeBuzz.Data
{
    public class BeeBuzzSeeder
    {
        private readonly ApplicationDbContext _db;

        private readonly IWebHostEnvironment _hosting;

        private readonly RoleManager<IdentityRole<int>> _roleManager;

        private readonly UserManager<ApplicationUser> _userManager;

        public BeeBuzzSeeder(ApplicationDbContext db, IWebHostEnvironment hosting, RoleManager<IdentityRole<int>> roleManager, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _hosting = hosting;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task Seed()
        {
            _db.Database.EnsureCreated();

            // 4. Create a default and admin role in seeding
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole<int>("Admin"));
                await _roleManager.CreateAsync(new IdentityRole<int>("Default"));
            }

            // 4. Create an Admin user in seeding
            ApplicationUser admin = await SeedAdminUser();

            if (!_db.Organizations.Any())
            {
                // Create a 0000-0000-0000-0000 Organization, and add the admin user to it
                Organization initialOrganization = new Organization();
                initialOrganization.OrganizationId = "0000-0000-0000-0000";

                initialOrganization.Users.Add(admin);

                _db.Organizations.Add(initialOrganization);
            }

        }

        private async Task<ApplicationUser> SeedAdminUser()
        {
            const string adminRoleName = "Admin";
            const string adminUsername = "admin";
            const string adminEmail = "admin@beebuzz.com";
            const string adminPassword = "Password123!";

            // Ensure the Admin role exists
            if (!await _roleManager.RoleExistsAsync(adminRoleName))
            {
                await _roleManager.CreateAsync(new IdentityRole<int>(adminRoleName));
            }

            // Check if admin user already exists
            var existingAdmin = await _userManager.FindByEmailAsync(adminEmail);
            if (existingAdmin == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = adminUsername,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(adminUser, adminPassword);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(adminUser, adminRoleName);

                    return adminUser;
                }
                else
                {
                    throw new Exception("Failed to create admin user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }

            return existingAdmin;
        }
    }
}
