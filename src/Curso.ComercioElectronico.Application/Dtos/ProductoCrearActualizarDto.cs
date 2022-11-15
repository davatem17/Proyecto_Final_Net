using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;


public class ProductoCrearActualizarDto
{
    [Required]
    [StringLength(DominioConstantes.NOMBRE_MAXIMO)]
    public string Nombre { get; set; }

    [Required]
    public decimal Precio { get; set; }
    [Required]
    [StringLength(DominioConstantes.OBSERVACIONESP_MAXIMO)]
    public string? Observaciones { get; set; }
    [Required]
    public DateTime? Caducidad { get; set; }
    [Required]
    public DateTime? FechaElaboracion { get; set; }
    [Required]
    public bool Disponible { get; set; }

    [Required]
    //[ForeignKey("Marca")]
    //EntidadId. Clave F. A la entidad 
    public int MarcaId { get; set; }

    [Required]
    public int TipoProductoId { get; set; }

    [Required]
    public int BodegaId { get; set; }

}