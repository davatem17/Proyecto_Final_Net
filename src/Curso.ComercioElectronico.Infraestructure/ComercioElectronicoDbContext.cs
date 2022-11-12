using Curso.ComercioElectronico.Domain;
using Microsoft.EntityFrameworkCore;

namespace Curso.ComercioElectronico.Infraestructure;

public class ComercioElectronicoDbContext:DbContext, IUnitOfWork
{

    //Agregar sus entidades
    public DbSet<Marca> Marcas {get;set;}

    public DbSet<Autor> Autors {get;set;}

    public DbSet<Editorial> Editorials {get;set;}

    public DbSet<Libro> Libros {get;set;}

    public string DbPath { get; set; }

    public ComercioElectronicoDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "curso.comercio-electronico-V1.db");
 
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

} 



