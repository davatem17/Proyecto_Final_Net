using System.ComponentModel.DataAnnotations;

namespace Curso.ComercioElectronico.Domain;


public class Marca
{
    [Required]
    public int Id {get;set;}

    [Required]
    [StringLength(DominioConstantes.NOMBRE_MAXIMO)]
    public string Nombre {get;set;}

    [Required]
    [StringLength(DominioConstantes.PAISORIGEN_MAXIMO)]
    public string PaisOrigen {get;set;}

    [Required]
    public bool PresenciaInternacional {get;set;}

}




