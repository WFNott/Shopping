
using Microsoft.AspNetCore.Identity;
using Shopping.Data.Entities;
using Shopping.Enum;
using Shopping.Helpers;

namespace Shopping.Data
{
    public class SeedDB
    {
        private readonly DataContex _context;
        private readonly IUserHelper _userHelper;

        public SeedDB(DataContex context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCategoriesAsync();
            await CheckCountriesAsync();
            await CheckRoleAsync();
            await CheckUserAsync("1010", "Juan", "Zuluaga", "zulu@yopmail.com", "322 311 4620", "Calle Luna Calle Sol", UserTypes.Admin);
            await CheckUserAsync("2020", "Samuel", "Acevedo Tarazona", "sam@yopmail.com", "322 311 4620", "Calle Luna Calle Sol", UserTypes.User);
        }

        private async Task<User> CheckUserAsync(
     string document,
     string firstName,
     string lastName,
     string email,
     string phone,
     string address,
     UserTypes userType)
        {
            User user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    City = _context.cities.FirstOrDefault(),
                    UserType = userType,
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());
            }

            return user;
        }


        private async Task CheckRoleAsync()
        {
            await _userHelper.CheckRoleAsync(UserTypes.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserTypes.User.ToString());

        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.countries.Any())
            {
                _context.countries.Add(new Country
                {
                    Name = "Colombia",
                    States = new List<State>()
                    {
                        new State()
                        {
                            Name = "Antioquia",
                            Cities = new List<City>() {
                                new City() { Name = "Medellín" },
                                new City() { Name = "Itagüí" },
                                new City() { Name = "Envigado" },
                                new City() { Name = "Bello" },
                                new City() { Name = "Rionegro" },
                            }
                        },
                        new State()
                        {
                            Name = "Bogotá",
                            Cities = new List<City>() {
                                new City() { Name = "Usaquen" },
                                new City() { Name = "Champinero" },
                                new City() { Name = "Santa fe" },
                                new City() { Name = "Useme" },
                                new City() { Name = "Bosa" },
                            }
                        },
                    }
                });
                _context.countries.Add(new Country
                {
                    Name = "Estados Unidos",
                    States = new List<State>()
                    {
                        new State()
                        {
                            Name = "Florida",
                            Cities = new List<City>() {
                                new City() { Name = "Orlando" },
                                new City() { Name = "Miami" },
                                new City() { Name = "Tampa" },
                                new City() { Name = "Fort Lauderdale" },
                                new City() { Name = "Key West" },
                            }
                        },
                        new State()
                        {
                            Name = "Texas",
                            Cities = new List<City>() {
                                new City() { Name = "Houston" },
                                new City() { Name = "San Antonio" },
                                new City() { Name = "Dallas" },
                                new City() { Name = "Austin" },
                                new City() { Name = "El Paso" },
                            }
                        },
                    }
                });
            }

            await _context.SaveChangesAsync();
        }



        private async Task CheckCategoriesAsync()
        {

            if (!_context.categories.Any())
            {
                _context.categories.Add(new Category { Name = "Tecnologia" });
                _context.categories.Add(new Category { Name = "Belleza" });
                _context.categories.Add(new Category { Name = "Nutricion" });
                _context.categories.Add(new Category { Name = "Medicina" });
                _context.categories.Add(new Category { Name = "Entretenimiento" });
                _context.categories.Add(new Category { Name = "Ropa" });
                _context.categories.Add(new Category { Name = "Mascota" });
                await _context.SaveChangesAsync();

            }

        }
    }
}
