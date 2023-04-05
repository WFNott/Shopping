using Microsoft.EntityFrameworkCore;
using Shopping.Data.Entities;

namespace Shopping.Data
{
    // Esta clase esta hecha para la conexion a la base de datos, DbCOntext es una clase aun mayor de Entity
    public class DataContex : DbContext
    {

        // Se crea el constructor donde se le pasa DbContextOptions (Una clase manipular el contexto para la conexion)

        public DataContex(DbContextOptions<DataContex> options): base(options)
        {
            
        }

        // Creo un DbSet para mapear la entidad, esto quiere decir que convierte a un objeto a una entidad
        // legible por el Entity framework y se le asigna el nombre 

        public DbSet<Country> countries { get; set; }

        public DbSet<Category> categories { get; set; }



        // El "OnModelCreating" es un metodo para modificar o crear en la tabla
        // se le asigno una variable del tipo "ModelBuilder" llamada modelBuilder
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Se modificara en la base lo indicado dentro de la variable modelBuilder
            base.OnModelCreating(modelBuilder);

            // modelBuilder dice que la entidad country tiene un indice el cual es que 
            // no se repite ningun nombre dentro de la entidad 

            modelBuilder.Entity<Category>(entity => { entity.HasIndex(c => c.Name).IsUnique(); });

            modelBuilder.Entity<Country>(entity =>{ entity.HasIndex(c => c.Name).IsUnique();});
        }

    }

}
