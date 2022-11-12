using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;

 
public class LibroDto
{
    [Required]
    public int Id {get;set;}

    [Required]
    [StringLength(DominioConstantes.NOMBRE_MAXIMO)]
    public string Nombre {get;set;}

    public DateTime FechaPublicacion {get;set;}

    [Required]
    public int AutorId {get;set;}
    public string Autor {get;set;}

    [Required]
    public int EditorialId {get;set;}
    public string Editorial {get;set;}
}
