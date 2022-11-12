using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;

 
public class EditorialDto
{
    [Required]
    public int Id {get;set;}

    [Required]
    [StringLength(DominioConstantes.NOMBRE_MAXIMO)]
    public string Nombre {get;set;}

    [Required]
    public string Ubicacion {get;set;}
}
