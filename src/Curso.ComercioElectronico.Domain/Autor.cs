using System.ComponentModel.DataAnnotations;

namespace Curso.ComercioElectronico.Domain;

public class Autor
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