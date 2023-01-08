using FudeeTestApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FudeeTestApi.Data
{
    public class FudeeSeeder
    {
        public static void Initiazlize(IServiceProvider serviceProvider)
        {
            using (var dbContext = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
                if (dbContext.Database.CanConnect())
                {
                    SeedRoles(dbContext);
                    SeedUsers(dbContext);
                    SeedCategories(dbContext);
                    SeedRestaurants(dbContext);
                    SeedOpinions(dbContext);
                    SeedDishes(dbContext);
                }
        }

        //zakładanie ról w aplikacji, o ile nie istnieją (admin, manager, uzytkownik)
        private static void SeedRoles(ApplicationDbContext dbContext)
        {
            var roleStore = new RoleStore<IdentityRole>(dbContext);
            if (!dbContext.Roles.Any(r => r.Name == "admin"))
            {
                roleStore.CreateAsync(new IdentityRole
                {
                    Name = "admin",
                    NormalizedName = "admin"
                }).Wait();
            }
            if (!dbContext.Roles.Any(r => r.Name == "manager"))
            {
                roleStore.CreateAsync(new IdentityRole
                {
                    Name = "manager",
                    NormalizedName = "manager"
                }).Wait();
            }
            if (!dbContext.Roles.Any(r => r.Name == "uzytkownik"))
            {
                roleStore.CreateAsync(new IdentityRole
                {
                    Name = "uzytkownik",
                    NormalizedName = "uzytkownik"
                }).Wait();
            }

        }// koniec ról

        //zakładanie kont uzytkowników w apliakcji, o ile nie istnieją
        private static void SeedUsers(ApplicationDbContext dbContext)
        {
            if (!dbContext.Users.Any(u => u.UserName == "manager1@fudee.pl"))
            {
                var user = new AppUser
                {
                    UserName = "manager1@fudee.pl",
                    NormalizedUserName = "manager1@fudee.pl",
                    Email = "manager1@fudee.pl",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    FirstName = "Emil",
                    LastName = "kucharski",
                    Photo = "manager1.jpg",
                    Information = "Lorem ipsum dolor sit amet. Est fugiat illo et quasi magnam qui sint numquam qui quia vero ut labore ipsa."
                };
                var password = new PasswordHasher<AppUser>();
                var hashed = password.HashPassword(user, "Fudee1!");
                user.PasswordHash = hashed;

                var userStore = new UserStore<AppUser>(dbContext);
                userStore.CreateAsync(user).Wait();
                userStore.AddToRoleAsync(user, "manager").Wait();

                dbContext.SaveChanges();
            }

            if (!dbContext.Users.Any(u => u.UserName == "manager2@fudee.pl"))
            {
                var user = new AppUser
                {
                    UserName = "manager2@fudee.pl",
                    NormalizedUserName = "manager2@fudee.pl",
                    Email = "manager2@fudee.pl",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    FirstName = "Sara",
                    LastName = "Warzywko",
                    Photo = "manager2.jpg",
                    Information = "Lorem ipsum dolor sit amet. Est fugiat illo et quasi magnam qui sint numquam qui quia vero ut labore ipsa."
                };
                var password = new PasswordHasher<AppUser>();
                var hashed = password.HashPassword(user, "Fudee1!");
                user.PasswordHash = hashed;

                var userStore = new UserStore<AppUser>(dbContext);
                userStore.CreateAsync(user).Wait();
                userStore.AddToRoleAsync(user, "manager").Wait();

                dbContext.SaveChanges();
            }

            if (!dbContext.Users.Any(u => u.UserName == "uzytkownik@fudee.pl"))
            {
                var user = new AppUser
                {
                    UserName = "uzytkownik@fudee.pl",
                    NormalizedUserName = "uzytkownik@fudee.pl",
                    Email = "uzytkownik@fudee.pl",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    FirstName = "Adam",
                    LastName = "Boczek",
                    Photo = "man.png",
                    Information = "Lorem ipsum dolor sit amet."
                };
                var password = new PasswordHasher<AppUser>();
                var hashed = password.HashPassword(user, "Fudee1!");
                user.PasswordHash = hashed;

                var userStore = new UserStore<AppUser>(dbContext);
                userStore.CreateAsync(user).Wait();
                userStore.AddToRoleAsync(user, "uzytkownik").Wait();
                dbContext.SaveChanges();
            }

            if (!dbContext.Users.Any(u => u.UserName == "admin@fudee.pl"))
            {
                var user = new AppUser
                {
                    UserName = "admin@fudee.pl",
                    NormalizedUserName = "admin@fudee.pl",
                    Email = "admin@fudee.pl",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    FirstName = "Ewa",
                    LastName = "Ważna",
                    Photo = "woman.png",
                    Information = ""
                };
                var password = new PasswordHasher<AppUser>();
                var hashed = password.HashPassword(user, "Fudee1!");
                user.PasswordHash = hashed;

                var userStore = new UserStore<AppUser>(dbContext);
                userStore.CreateAsync(user).Wait();
                userStore.AddToRoleAsync(user, "admin").Wait();
                dbContext.SaveChanges();
            }
        } // koniec użytkowników

        //dodawanie danych kategorii
        private static void SeedCategories(ApplicationDbContext dbContext)
        {
            if (!dbContext.Categories.Any())
            {
                var kat = new List<Category>
                {
                    new Category { NameCategory = "Kawiarnie", Active = true, Display=true, Icon="1.png", DescriptionCategory="Aut incidunt architecto et adipisci doloribus et quis temporibus cum consequuntur corporis." },
                    new Category { NameCategory = "Fast Food", Active = true, Display=true, Icon="2.png", DescriptionCategory="Aut incidunt architecto et adipisci doloribus et quis temporibus cum consequuntur corporis." },
                    new Category { NameCategory = "Kuchnia domowa", Active = true, Display=true, Icon="3.png", DescriptionCategory="Aut incidunt architecto et adipisci doloribus et quis temporibus cum consequuntur corporis." },
                    new Category { NameCategory = "Pizza", Active = true, Display=true, Icon="4.png", DescriptionCategory="Aut incidunt architecto et adipisci doloribus et quis temporibus cum consequuntur corporis." },
                    new Category { NameCategory = "Kuchnia azjatycka", Active = true, Display=true, Icon="5.png", DescriptionCategory="Aut incidunt architecto et adipisci doloribus et quis temporibus cum consequuntur corporis." },
                    new Category { NameCategory = "Inne smaki", Active = true, Display=true, Icon="6.png", DescriptionCategory="Aut incidunt architecto et adipisci doloribus et quis temporibus cum consequuntur corporis." }
                };
                dbContext.AddRange(kat);
                dbContext.SaveChanges();
            }
        } //koniec danych kategorii

        //dodawanie danych restauracji i adresów
        private static void SeedRestaurants(ApplicationDbContext dbContext)
        {

            if (!dbContext.Restaurants.Any())
            {
                for (int i = 1; i <= 6; i++)
                {
                    var idUzytkownika1 = dbContext.AppUsers
                        .Where(u => u.UserName == "manager1@fudee.pl")
                        .FirstOrDefault()
                        .Id;
                    for (int j = 0; j <= 2; j++)
                    {
                        var restauracja = new Restaurant()
                        {
                            RestaurantName = "Restauracja" + i.ToString() + j.ToString(),
                            Address = new Address
                            {
                                City = "Płock",
                                StreetName = "Ulica" + i.ToString() + j.ToString(),
                                StreetNr = i.ToString() + j.ToString(),
                                LocalNr = i.ToString(),
                                PostalCode = "09-402",
                                ContactEmail = "restauracja" + i.ToString() + j.ToString() + "@poczta.pl",
                                ContactPhone = "5088552" + i.ToString() + j.ToString()
                            },
                            RestaurantDescription = "Ea possimus voluptas aut enim praesentium qui mollitia aspernatur sed modi odit! Quo consequatur voluptatem sit consequuntur consequuntur sit error obcaecati et nulla modi et ullam dolores ad quibusdam pariatur et veritatis molestiae. ",
                            OpenHours = DateTime.Now.AddHours(10).AddMinutes(00).AddSeconds(00),
                            CloseHours = DateTime.Now.AddHours(20).AddMinutes(00).AddSeconds(00),
                            HasDelivery = true,
                            HasCatering = true,
                            Events = true,
                            CategoryId = i,
                            AddedDate = DateTime.Now.AddDays(-i * j),
                            Id = idUzytkownika1

                        };
                        dbContext.Set<Restaurant>().Add(restauracja);
                    }
                    dbContext.SaveChanges();

                    var idUzytkownika2 = dbContext.AppUsers
                        .Where(u => u.UserName == "manager2@fudee.pl")
                        .FirstOrDefault()
                        .Id;
                    for (int j = 3; j <= 5; j++)
                    {
                        var restauracja = new Restaurant()
                        {
                            RestaurantName = "Restauracja" + i.ToString() + j.ToString(),
                            Address = new Address
                            {
                                City = "Płock",
                                StreetName = "Ulica" + i.ToString() + j.ToString(),
                                StreetNr = i.ToString() + j.ToString(),
                                LocalNr = i.ToString(),
                                PostalCode = "09-402",
                                ContactEmail = "restauracja" + i.ToString() + j.ToString() + "@poczta.pl",
                                ContactPhone = "5088552" + i.ToString() + j.ToString()
                            },
                            RestaurantDescription = "Ea possimus voluptas aut enim praesentium qui mollitia aspernatur sed modi odit! Quo consequatur voluptatem sit consequuntur consequuntur sit error obcaecati et nulla modi et ullam dolores ad quibusdam pariatur et veritatis molestiae. ",
                            OpenHours = DateTime.Now.AddHours(10).AddMinutes(00).AddSeconds(00),
                            CloseHours = DateTime.Now.AddHours(20).AddMinutes(00).AddSeconds(00),
                            HasDelivery = true,
                            HasCatering = true,
                            Events = true,
                            CategoryId = i,
                            AddedDate = DateTime.Now.AddDays(-i * j),
                            Id = idUzytkownika2,
                        };
                        dbContext.Set<Restaurant>().Add(restauracja);
                    }
                    dbContext.SaveChanges();
                }

            }

        }
        //dodawanie treści opinii, o ile nie istnieją
        private static void SeedOpinions(ApplicationDbContext dbContext)
        {
            if (!dbContext.Opinions.Any())
            {
                var idUzytkownika1 = dbContext.AppUsers
                .Where(u => u.UserName == "manager2@fudee.pl").FirstOrDefault()
                .Id;

                for (int i = 1; i <= 36; i++) //36 restauracji
                {
                    var komentarz = new Opinion()
                    {
                        Comment = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo.",
                        AddedDate = DateTime.Now.AddDays(-i),
                        Id = idUzytkownika1,
                        RestaurantId = i,
                        Rating = (TypeOfGrade?)5
                    };
                    dbContext.Set<Opinion>().Add(komentarz);
                }
                dbContext.SaveChanges();

                var idUzytkownika2 = dbContext.AppUsers
                .Where(u => u.UserName == "manager1@fudee.pl").FirstOrDefault()
                .Id;

                for (int i = 1; i <= 36; i++)
                {
                    var komentarz = new Opinion()
                    {
                        Comment = "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati cupiditate non provident.",
                        AddedDate = DateTime.Now.AddDays(-i),
                        Id = idUzytkownika2,
                        RestaurantId = i,
                        Rating = (TypeOfGrade?)4
                    };
                    dbContext.Set<Opinion>().Add(komentarz);
                }
                dbContext.SaveChanges();
            }
        } //koniec treści opinii

        //dodawanie dań do restauracji
        private static void SeedDishes(ApplicationDbContext dbContext)
        {
            if (!dbContext.Dishes.Any())
            {
                /*for (int i = 1; i < 36; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        var danie = new List<Dish>
                        {
                            new Dish { NameDish = "Danie"+j.ToString(), Image = "1.jpg", Price = 15.5+j*2, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 1 },
                        };
                        dbContext.AddRange(danie);
                    }
                    
                    dbContext.SaveChanges();

                }*/
                var danie = new List<Dish>
                {
                    new Dish { NameDish = "Danie1", Image = "1.jpg", Price = 15.5, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 1 },
                    new Dish { NameDish = "Danie2", Image = "2.jpg", Price = 20, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 1 },
                    new Dish { NameDish = "Danie3", Image = "3.jpg", Price = 40, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 1 },
                    new Dish { NameDish = "Danie4", Image = "4.jpg", Price = 15.5, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 1 },
                    new Dish { NameDish = "Danie5", Image = "5.jpg", Price = 20.5, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 1 },
                    new Dish { NameDish = "Danie6", Image = "6.jpg", Price = 16, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 1 },
                    new Dish { NameDish = "Danie7", Image = "7.jpg", Price = 16, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 1 },
                    new Dish { NameDish = "Danie8", Image = "8.jpg", Price = 50, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 1 },
                    new Dish { NameDish = "Danie9", Image = "9.jpg", Price = 25, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 1 },
                    new Dish { NameDish = "Danie1", Image = "10.jpg", Price = 18, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 1 },
                    new Dish { NameDish = "Danie2", Image = "11.jpg", Price = 69, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 2 },
                    new Dish { NameDish = "Danie3", Image = "12.jpg", Price = 28, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 2 },
                    new Dish { NameDish = "Danie5", Image = "13.jpg", Price = 12, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 2 },
                    new Dish { NameDish = "Danie6", Image = "14.jpg", Price = 20, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 2 },
                    new Dish { NameDish = "Danie7", Image = "15.jpg", Price = 11, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 2 },
                    new Dish { NameDish = "Danie8", Image = "16.jpg", Price = 8, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 2 },
                    new Dish { NameDish = "Danie9", Image = "17.jpg", Price = 25, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 2 },
                    new Dish { NameDish = "Danie10", Image = "18.jpg", Price = 30.5, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 2 },
                    new Dish { NameDish = "Danie1", Image = "1.jpg", Price = 15.5, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 3 },
                    new Dish { NameDish = "Danie2", Image = "2.jpg", Price = 20, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 3 },
                    new Dish { NameDish = "Danie3", Image = "3.jpg", Price = 40, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 3 },
                    new Dish { NameDish = "Danie4", Image = "4.jpg", Price = 15.5, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 3 },
                    new Dish { NameDish = "Danie5", Image = "5.jpg", Price = 20.5, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 3 },
                    new Dish { NameDish = "Danie6", Image = "6.jpg", Price = 16, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 4 },
                    new Dish { NameDish = "Danie7", Image = "7.jpg", Price = 16, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 4 },
                    new Dish { NameDish = "Danie8", Image = "8.jpg", Price = 50, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 4 },
                    new Dish { NameDish = "Danie9", Image = "9.jpg", Price = 25, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 4 },
                    new Dish { NameDish = "Danie1", Image = "10.jpg", Price = 18, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 5 },
                    new Dish { NameDish = "Danie2", Image = "11.jpg", Price = 69, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 5 },
                    new Dish { NameDish = "Danie3", Image = "12.jpg", Price = 28, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 5 },
                    new Dish { NameDish = "Danie5", Image = "13.jpg", Price = 12, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 5 },
                    new Dish { NameDish = "Danie6", Image = "14.jpg", Price = 20, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 5 },
                    new Dish { NameDish = "Danie7", Image = "15.jpg", Price = 11, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 6 },
                    new Dish { NameDish = "Danie8", Image = "16.jpg", Price = 8, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 6 },
                    new Dish { NameDish = "Danie9", Image = "17.jpg", Price = 25, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 6 },
                    new Dish { NameDish = "Danie10", Image = "18.jpg", Price = 30.5, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 6 },
                    new Dish { NameDish = "Danie1", Image = "1.jpg", Price = 15.5, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 7 },
                    new Dish { NameDish = "Danie2", Image = "2.jpg", Price = 20, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 7 },
                    new Dish { NameDish = "Danie3", Image = "3.jpg", Price = 40, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 7 },
                    new Dish { NameDish = "Danie4", Image = "4.jpg", Price = 15.5, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 7 },
                    new Dish { NameDish = "Danie5", Image = "5.jpg", Price = 20.5, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 7 },
                    new Dish { NameDish = "Danie6", Image = "6.jpg", Price = 16, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 7 },
                    new Dish { NameDish = "Danie7", Image = "7.jpg", Price = 16, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 8 },
                    new Dish { NameDish = "Danie8", Image = "8.jpg", Price = 50, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 8 },
                    new Dish { NameDish = "Danie9", Image = "9.jpg", Price = 25, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 8 },
                    new Dish { NameDish = "Danie1", Image = "10.jpg", Price = 18, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 8 },
                    new Dish { NameDish = "Danie2", Image = "11.jpg", Price = 69, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 8 },
                    new Dish { NameDish = "Danie3", Image = "12.jpg", Price = 28, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 8 },
                    new Dish { NameDish = "Danie5", Image = "13.jpg", Price = 12, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 9 },
                    new Dish { NameDish = "Danie6", Image = "14.jpg", Price = 20, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 9 },
                    new Dish { NameDish = "Danie7", Image = "15.jpg", Price = 11, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 9 },
                    new Dish { NameDish = "Danie8", Image = "16.jpg", Price = 8, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 9 },
                    new Dish { NameDish = "Danie9", Image = "17.jpg", Price = 25, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 9 },
                    new Dish { NameDish = "Danie10", Image = "18.jpg", Price = 30.5, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 9 },
                    new Dish { NameDish = "Danie1", Image = "1.jpg", Price = 15.5, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 9 },
                    new Dish { NameDish = "Danie2", Image = "2.jpg", Price = 20, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 10 },
                    new Dish { NameDish = "Danie3", Image = "3.jpg", Price = 40, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 10 },
                    new Dish { NameDish = "Danie4", Image = "4.jpg", Price = 15.5, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 10 },
                    new Dish { NameDish = "Danie5", Image = "5.jpg", Price = 20.5, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 10 },
                    new Dish { NameDish = "Danie6", Image = "6.jpg", Price = 16, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 10 },
                    new Dish { NameDish = "Danie7", Image = "7.jpg", Price = 16, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 11 },
                    new Dish { NameDish = "Danie8", Image = "8.jpg", Price = 50, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 12 },
                    new Dish { NameDish = "Danie9", Image = "9.jpg", Price = 25, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 13 },
                    new Dish { NameDish = "Danie1", Image = "10.jpg", Price = 18, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 14 },
                    new Dish { NameDish = "Danie2", Image = "11.jpg", Price = 69, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 14 },
                    new Dish { NameDish = "Danie3", Image = "12.jpg", Price = 28, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 14 },
                    new Dish { NameDish = "Danie5", Image = "13.jpg", Price = 12, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 11 },
                    new Dish { NameDish = "Danie6", Image = "14.jpg", Price = 20, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 11 },
                    new Dish { NameDish = "Danie7", Image = "15.jpg", Price = 11, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 11 },
                    new Dish { NameDish = "Danie8", Image = "16.jpg", Price = 8, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 12 },
                    new Dish { NameDish = "Danie9", Image = "17.jpg", Price = 25, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 12 },
                    new Dish { NameDish = "Danie10", Image = "18.jpg", Price = 30.5, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 12 },
                    new Dish { NameDish = "Danie1", Image = "1.jpg", Price = 15.5, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 12 },
                    new Dish { NameDish = "Danie2", Image = "2.jpg", Price = 20, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 12 },
                    new Dish { NameDish = "Danie3", Image = "3.jpg", Price = 40, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 13 },
                    new Dish { NameDish = "Danie4", Image = "4.jpg", Price = 15.5, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 13 },
                    new Dish { NameDish = "Danie5", Image = "5.jpg", Price = 20.5, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 13 },
                    new Dish { NameDish = "Danie6", Image = "6.jpg", Price = 16, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 13 },
                    new Dish { NameDish = "Danie7", Image = "7.jpg", Price = 16, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 14 },
                    new Dish { NameDish = "Danie8", Image = "8.jpg", Price = 50, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 15 },
                    new Dish { NameDish = "Danie9", Image = "9.jpg", Price = 25, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 15 },
                    new Dish { NameDish = "Danie1", Image = "10.jpg", Price = 18, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 15 },
                    new Dish { NameDish = "Danie2", Image = "11.jpg", Price = 69, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 21 },
                    new Dish { NameDish = "Danie3", Image = "12.jpg", Price = 28, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 21 },
                    new Dish { NameDish = "Danie5", Image = "13.jpg", Price = 12, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 21 },
                    new Dish { NameDish = "Danie6", Image = "14.jpg", Price = 20, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 21 },
                    new Dish { NameDish = "Danie7", Image = "15.jpg", Price = 11, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 21 },
                    new Dish { NameDish = "Danie8", Image = "16.jpg", Price = 8, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 22 },
                    new Dish { NameDish = "Danie9", Image = "17.jpg", Price = 25, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 22 },
                    new Dish { NameDish = "Danie10", Image = "18.jpg", Price = 30.5, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 22 },
                    new Dish { NameDish = "Danie1", Image = "1.jpg", Price = 15.5, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 23 },
                    new Dish { NameDish = "Danie2", Image = "2.jpg", Price = 20, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 23 },
                    new Dish { NameDish = "Danie3", Image = "3.jpg", Price = 40, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 23 },
                    new Dish { NameDish = "Danie4", Image = "4.jpg", Price = 15.5, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 23 },
                    new Dish { NameDish = "Danie5", Image = "5.jpg", Price = 20.5, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 23 },
                    new Dish { NameDish = "Danie6", Image = "6.jpg", Price = 16, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 24 },
                    new Dish { NameDish = "Danie7", Image = "7.jpg", Price = 16, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 24 },
                    new Dish { NameDish = "Danie8", Image = "8.jpg", Price = 50, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 24 },
                    new Dish { NameDish = "Danie9", Image = "9.jpg", Price = 25, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 24 },
                    new Dish { NameDish = "Danie1", Image = "10.jpg", Price = 18, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 25 },
                    new Dish { NameDish = "Danie2", Image = "11.jpg", Price = 69, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 25 },
                    new Dish { NameDish = "Danie3", Image = "12.jpg", Price = 28, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 25 },
                    new Dish { NameDish = "Danie5", Image = "13.jpg", Price = 12, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 25 },
                    new Dish { NameDish = "Danie6", Image = "14.jpg", Price = 20, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 25 },
                    new Dish { NameDish = "Danie7", Image = "15.jpg", Price = 11, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 26 },
                    new Dish { NameDish = "Danie8", Image = "16.jpg", Price = 8, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 26 },
                    new Dish { NameDish = "Danie9", Image = "17.jpg", Price = 25, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 26 },
                    new Dish { NameDish = "Danie10", Image = "18.jpg", Price = 30.5, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 26 },
                    new Dish { NameDish = "Danie1", Image = "1.jpg", Price = 15.5, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 27 },
                    new Dish { NameDish = "Danie2", Image = "2.jpg", Price = 20, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 27 },
                    new Dish { NameDish = "Danie3", Image = "3.jpg", Price = 40, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 27 },
                    new Dish { NameDish = "Danie4", Image = "4.jpg", Price = 15.5, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 27 },
                    new Dish { NameDish = "Danie5", Image = "5.jpg", Price = 20.5, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 27 },
                    new Dish { NameDish = "Danie6", Image = "6.jpg", Price = 16, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 27 },
                    new Dish { NameDish = "Danie7", Image = "7.jpg", Price = 16, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 28 },
                    new Dish { NameDish = "Danie8", Image = "8.jpg", Price = 50, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 28 },
                    new Dish { NameDish = "Danie9", Image = "9.jpg", Price = 25, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 28 },
                    new Dish { NameDish = "Danie1", Image = "10.jpg", Price = 18, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 28 },
                    new Dish { NameDish = "Danie2", Image = "11.jpg", Price = 69, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 28 },
                    new Dish { NameDish = "Danie3", Image = "12.jpg", Price = 28, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 28 },
                    new Dish { NameDish = "Danie5", Image = "13.jpg", Price = 12, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 29 },
                    new Dish { NameDish = "Danie6", Image = "14.jpg", Price = 20, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 29 },
                    new Dish { NameDish = "Danie7", Image = "15.jpg", Price = 11, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 29 },
                    new Dish { NameDish = "Danie8", Image = "16.jpg", Price = 8, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 29 },
                    new Dish { NameDish = "Danie9", Image = "17.jpg", Price = 25, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 29 },
                    new Dish { NameDish = "Danie10", Image = "18.jpg", Price = 30.5, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 29 },
                    new Dish { NameDish = "Danie1", Image = "1.jpg", Price = 15.5, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 29 },
                    new Dish { NameDish = "Danie2", Image = "2.jpg", Price = 20, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 15 },
                    new Dish { NameDish = "Danie3", Image = "3.jpg", Price = 40, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 16 },
                    new Dish { NameDish = "Danie4", Image = "4.jpg", Price = 15.5, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 16 },
                    new Dish { NameDish = "Danie5", Image = "5.jpg", Price = 20.5, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 16 },
                    new Dish { NameDish = "Danie6", Image = "6.jpg", Price = 16, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 16 },
                    new Dish { NameDish = "Danie7", Image = "7.jpg", Price = 16, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 16 },
                    new Dish { NameDish = "Danie8", Image = "8.jpg", Price = 50, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 17 },
                    new Dish { NameDish = "Danie9", Image = "9.jpg", Price = 25, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 17 },
                    new Dish { NameDish = "Danie1", Image = "10.jpg", Price = 18, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 17 },
                    new Dish { NameDish = "Danie2", Image = "11.jpg", Price = 69, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 17 },
                    new Dish { NameDish = "Danie3", Image = "12.jpg", Price = 28, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 17 },
                    new Dish { NameDish = "Danie5", Image = "13.jpg", Price = 12, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 18 },
                    new Dish { NameDish = "Danie6", Image = "14.jpg", Price = 20, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 18 },
                    new Dish { NameDish = "Danie7", Image = "15.jpg", Price = 11, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 18 },
                    new Dish { NameDish = "Danie8", Image = "16.jpg", Price = 8, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 18 },
                    new Dish { NameDish = "Danie9", Image = "17.jpg", Price = 25, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 18 },
                    new Dish { NameDish = "Danie10", Image = "18.jpg", Price = 30.5, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 18 },
                    new Dish { NameDish = "Danie2", Image = "2.jpg", Price = 20, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 31 },
                    new Dish { NameDish = "Danie3", Image = "3.jpg", Price = 40, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 31 },
                    new Dish { NameDish = "Danie4", Image = "4.jpg", Price = 15.5, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 31 },
                    new Dish { NameDish = "Danie5", Image = "5.jpg", Price = 20.5, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 31 },
                    new Dish { NameDish = "Danie6", Image = "6.jpg", Price = 16, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 31 },
                    new Dish { NameDish = "Danie7", Image = "7.jpg", Price = 16, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 31 },
                    new Dish { NameDish = "Danie8", Image = "8.jpg", Price = 50, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 31 },
                    new Dish { NameDish = "Danie9", Image = "9.jpg", Price = 25, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 31 },
                    new Dish { NameDish = "Danie1", Image = "10.jpg", Price = 18, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 31 },
                    new Dish { NameDish = "Danie2", Image = "11.jpg", Price = 69, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 32 },
                    new Dish { NameDish = "Danie3", Image = "12.jpg", Price = 28, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 32 },
                    new Dish { NameDish = "Danie5", Image = "13.jpg", Price = 12, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 32 },
                    new Dish { NameDish = "Danie6", Image = "14.jpg", Price = 20, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 32 },
                    new Dish { NameDish = "Danie7", Image = "15.jpg", Price = 11, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 32 },
                    new Dish { NameDish = "Danie8", Image = "16.jpg", Price = 8, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 32 },
                    new Dish { NameDish = "Danie9", Image = "17.jpg", Price = 25, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 32 },
                    new Dish { NameDish = "Danie10", Image = "18.jpg", Price = 30.5, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 32 },
                    new Dish { NameDish = "Danie1", Image = "1.jpg", Price = 15.5, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 33 },
                    new Dish { NameDish = "Danie2", Image = "2.jpg", Price = 20, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 33 },
                    new Dish { NameDish = "Danie3", Image = "3.jpg", Price = 40, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 33 },
                    new Dish { NameDish = "Danie4", Image = "4.jpg", Price = 15.5, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 33 },
                    new Dish { NameDish = "Danie5", Image = "5.jpg", Price = 20.5, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 33 },
                    new Dish { NameDish = "Danie6", Image = "6.jpg", Price = 16, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 34 },
                    new Dish { NameDish = "Danie7", Image = "7.jpg", Price = 16, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 34 },
                    new Dish { NameDish = "Danie8", Image = "8.jpg", Price = 50, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 34 },
                    new Dish { NameDish = "Danie9", Image = "9.jpg", Price = 25, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 34 },
                    new Dish { NameDish = "Danie1", Image = "10.jpg", Price = 18, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 35 },
                    new Dish { NameDish = "Danie2", Image = "11.jpg", Price = 69, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 35 },
                    new Dish { NameDish = "Danie3", Image = "12.jpg", Price = 28, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 35 },
                    new Dish { NameDish = "Danie5", Image = "13.jpg", Price = 12, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 35 },
                    new Dish { NameDish = "Danie6", Image = "14.jpg", Price = 20, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 35 },
                    new Dish { NameDish = "Danie7", Image = "15.jpg", Price = 11, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 36 },
                    new Dish { NameDish = "Danie8", Image = "16.jpg", Price = 8, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 36 },
                    new Dish { NameDish = "Danie9", Image = "17.jpg", Price = 25, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 36 },
                    new Dish { NameDish = "Danie10", Image = "18.jpg", Price = 30.5, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 36 },
                    new Dish { NameDish = "Danie7", Image = "15.jpg", Price = 11, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 19 },
                    new Dish { NameDish = "Danie8", Image = "16.jpg", Price = 8, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 19 },
                    new Dish { NameDish = "Danie9", Image = "17.jpg", Price = 25, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 19 },
                    new Dish { NameDish = "Danie10", Image = "18.jpg", Price = 30.5, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 19 },
                    new Dish { NameDish = "Danie1", Image = "1.jpg", Price = 15.5, DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui", RestaurantId = 19 },

                };
                dbContext.AddRange(danie);
                dbContext.SaveChanges();
                /*for (int i = 1; i <= 36; i++) //36 restauracji
                {

                    for (int j = 0; j < 7; j++)
                    {
                        var danie = new Dish()
                        {
                            NameDish = "Sed ut perspiciatis",
                            DescriptionDish = "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias exc",
                            Price = 15.0,
                            RestaurantId = i,
                            Image = "danie.jpg"
                        };
                        dbContext.Set<Dish>().Add(danie);
                    }
                    
                }
                dbContext.SaveChanges();*/

            }
        }//koniec dań

    }
}
