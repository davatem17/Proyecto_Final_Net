using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;

 
public class AutorDto
{
    [Required]
    public int Id {get;set;}

    [Required]
    [StringLength(DominioConstantes.NOMBRE_MAXIMO)]
    public string Nombre {get;set;}

    [Required]
    public int Edad {get;set;} 

    [Required]
    public string Domicilio {get;set;}
}
