using System.ComponentModel.DataAnnotations;

namespace Curso.ComercioElectronico.Domain;


public class Producto
{

    [Required]
    public Guid Id { get; set; }

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
    public virtual Marca Marca { get; set; }
    [Required]
    public int TipoProductoId { get; set; }
    public virtual TipoProducto TipoProducto { get; set; }
    [Required]
    public int BodegaId { get; set; }
    public virtual Bodega Bodega { get; set; }

}

