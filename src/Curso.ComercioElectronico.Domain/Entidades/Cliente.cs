using System.ComponentModel.DataAnnotations;

namespace Curso.ComercioElectronico.Domain;


public class Cliente
{
    [Required]
    public Guid Id {get;set; }

    [Required]
    [StringLength(DominioConstantes.CEDULA_MAXIMO)]
    public string Cedula {get;set;}

    [Required]
    [StringLength(DominioConstantes.NOMBRE_MAXIMO)]
    public string Nombre {get;set;}

    [Required]
    [StringLength(DominioConstantes.APELLIDO_MAXIMO)]
    public string Apellido {get;set;}

    [Required]
    [StringLength(DominioConstantes.TELEFONO_MAXIMO)]
    public string Telefono {get;set;}

    [Required]
    [StringLength(DominioConstantes.CORREO_MAXIMO)]
    public string CorreoElectronico {get;set;}

    [Required]
    [StringLength(DominioConstantes.DIRECCION_MAXIMO)]
    public string Direccion {get;set;}


}