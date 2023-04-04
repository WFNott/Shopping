using Microsoft.EntityFrameworkCore;
using Shopping.Data.Entities;

namespace Shopping.Data
{
    // Esta clase esta hecha para la conexion a la base de datos
    public class DataContex : DbContext
    {

        // Se crea un constructor

        public DataContex(DbContextOptions<DataContex> options): base(options)
        {
            
        }

        // Creo un DbSet para mapear la entidad, esto quiere decir que convierte a un objeto a una entidad
        // legible por el Entity framework y se le asigna el nombre 

        public DbSet<Country> countries { get; set; }


        // El "OnModelCreating" es un metodo para modificar o crear en la tabla
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Country>().HasIndex(c => c.Name).IsUnique();
        }

    }

}
