using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;


public class BodegaDto
{
    [Required]
    public int Id {get;set;}

    [Required]
    [StringLength(DominioConstantes.NOMBRE_MAXIMO)]
    public string Nombre {get;set;}

    [Required]
    [StringLength(DominioConstantes.UBICACION_MAXIMO)]
    public string Ubicacion {get;set;}

    [Required]
    [StringLength(DominioConstantes.TELEFONO_MAXIMO)]
    public string Telefono {get;set;}

}
