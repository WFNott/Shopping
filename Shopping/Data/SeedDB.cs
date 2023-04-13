
using Shopping.Data.Entities;

namespace Shopping.Data
{
    public class SeedDB
    {
        private readonly DataContex _context;

        public SeedDB(DataContex context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCategoriesAsync();
            await CheckCountriesAsync();
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
