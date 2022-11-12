using System.ComponentModel.DataAnnotations;

namespace Curso.ComercioElectronico.Domain;

public class Editorial
{
    [Required]
    public int Id {get;set;}

    [Required]
    [StringLength(DominioConstantes.NOMBRE_MAXIMO)]
    public string Nombre {get;set;}

    [Required]
    public string Ubicacion {get;set;}
}