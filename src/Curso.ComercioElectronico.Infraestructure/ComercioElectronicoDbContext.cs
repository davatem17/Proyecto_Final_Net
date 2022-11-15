using Curso.ComercioElectronico.Domain;
using Microsoft.EntityFrameworkCore;

namespace Curso.ComercioElectronico.Infraestructure;

public class ComercioElectronicoDbContext : DbContext, IUnitOfWork
{

    //Agregar sus entidades
    public DbSet<Marca> Marcas { get; set; }

    public DbSet<TipoProducto> TipoProductos { get; set; }

    public DbSet<Bodega> Bodegas {get;set;}
    public DbSet<Producto> Productos {get;set;}
 
  
    public string DbPath { get; set; }

    public ComercioElectronicoDbContext(DbContextOptions<ComercioElectronicoDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
         //#Ref: https://learn.microsoft.com/en-us/ef/core/providers/sqlite/limitations#query-limitations
          modelBuilder.Entity<Producto>()
            .Property(e => e.Precio)
            .HasConversion<double>()
            ;

           /*
            modelBuilder.Entity<OrdenItem>()
            .Property(e => e.Id)
            .HasConversion<string>();
          */

          //TODO: Conversion. Ejemplos. Estado. ??
      

    }

}




