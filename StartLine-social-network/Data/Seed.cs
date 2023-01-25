using Microsoft.AspNetCore.Identity;
using StartLine_social_network.Data.Enum;
using StartLine_social_network.Models;

namespace StartLine_social_network.Data
{
    public class Seed
    {
        // Generating random seeds to simply working with data

        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();

                // Clubs

                if (!context.Clubs.Any())
                {
                    context.Clubs.AddRange(new List<Club>()
                    {
                        new Club()
                        {
                            Title = "Club 1",
                            Image = "https://lh3.googleusercontent.com/p/AF1QipPfj9SmhAeGk1lP1RmYmiSvRIYXyyWPuYRJRH93=w1080-h608-p-no-v0",
                            Description = "The club description",
                            ClubCategory = ClubCategory.Drinking,
                            Address = new Address()
                            {
                                Street = "Długa 12",
                                City = "Kraków",
                                Province = "Małopolska"
                            }
                         },
                        new Club()
                        {
                            Title = "Club 2",
                            Image = "https://lh3.googleusercontent.com/p/AF1QipPfj9SmhAeGk1lP1RmYmiSvRIYXyyWPuYRJRH93=w1080-h608-p-no-v0",
                            Description = "The club description",
                            ClubCategory = ClubCategory.Everything,
                            Address = new Address()
                            {
                                Street = "Długa 12",
                                City = "Kraków",
                                Province = "Małopolska"
                            }
                        },
                        new Club()
                        {
                            Title = " Club 3",
                            Image = "https://lh3.googleusercontent.com/p/AF1QipPfj9SmhAeGk1lP1RmYmiSvRIYXyyWPuYRJRH93=w1080-h608-p-no-v0",
                            Description = "The club description",
                            ClubCategory = ClubCategory.Dancing,
                            Address = new Address()
                            {
                                Street = "Długa 12",
                                City = "Kraków",
                                Province = "Małopolska"
                            }
                        },
                        new Club()
                        {
                            Title = "Club 4",
                            Image = "https://lh3.googleusercontent.com/p/AF1QipPfj9SmhAeGk1lP1RmYmiSvRIYXyyWPuYRJRH93=w1080-h608-p-no-v0",
                            Description = "The club description",
                            ClubCategory = ClubCategory.HangOut,
                            Address = new Address()
                            {
                                Street = "Długa 12",
                                City = "Kraków",
                                Province = "Małopolska"
                            }
                        }
                    });
                    context.SaveChanges();
                }

                //  Parties
                if (!context.Parties.Any())
                {
                    context.Parties.AddRange(new List<Party>()
                    {
                        new Party()
                        {
                            Title = "Party 1",
                            Image = "https://lh3.googleusercontent.com/p/AF1QipPfj9SmhAeGk1lP1RmYmiSvRIYXyyWPuYRJRH93=w1080-h608-p-no-v0",
                            Description = "This is a party description",
                            PartyClubCategory = PartyClubCategory.Night,
                            Address = new Address()
                            {
                                Street = "Długa 12",
                                City = "Kraków",
                                Province = "Małopolska"
                            }
                        },
                        new Party()
                        {
                            Title = "Party 2",
                            Image = "https://lh3.googleusercontent.com/p/AF1QipPfj9SmhAeGk1lP1RmYmiSvRIYXyyWPuYRJRH93=w1080-h608-p-no-v0",
                            Description = "This a party description",
                            PartyClubCategory = PartyClubCategory.Pub,
                            AddressId = 5,
                            Address = new Address()
                            {
                               Street = "Długa 12",
                                City = "Kraków",
                                Province = "Małopolska"
                            }
                        }
                    });
                    context.SaveChanges();
                }
            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "filip.copija1@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = "filipcopija",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            Street = "Krótka 10",
                            City = "Kraków",
                            Province = "Małopolska"
                        }
                    };
                    await userManager.CreateAsync(newAdminUser, "kochamasp");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@example.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = "testuser",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            Street = "test",
                            City = "test",
                            Province = "test"
                        }
                    };
                    await userManager.CreateAsync(newAppUser, "test123");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
