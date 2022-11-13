using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;

  
public class MarcaCrearActualizarDto
{

 
    [Required]
    [StringLength(DominioConstantes.NOMBRE_MAXIMO)]
    public string Nombre {get;set;}

    [Required]
    [StringLength(DominioConstantes.PAISORIGEN_MAXIMO)]
    public string PaisOrigen {get;set;}

    [Required]
    public bool PresenciaInternacional {get;set;}
}