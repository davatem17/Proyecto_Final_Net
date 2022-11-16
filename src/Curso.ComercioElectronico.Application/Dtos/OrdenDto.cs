using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;

public class OrdenDto{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public Guid ClienteId { get; set; }

    public string Cliente { get; set; }

    public virtual ICollection<OrdenItem> Items { get; set; } = new List<OrdenItem>();

    [Required]
    public DateTime Fecha { get; set; }

    public DateTime? FechaAnulacion { get; set; }


    [Required]
    public decimal Total { get; set; }
    [StringLength(DominioConstantes.OBSERVACIONESORDEN_MAXIMO)]

    public string? Observaciones { get; set; }

    [Required]
    public OrdenEstado Estado { get; set; }

    
}

public class OrdenItemDto
{
    [Required]
    [StringLength(DominioConstantes.STRINGID)]
    public string Id { get; set; }

    [Required]
    public Guid ProductoId { get; set; }

    public string Producto { get; set; }

    [Required]
    public int OrdenId { get; set; }

    public string Orden { get; set; }

    [Required]
    public long Cantidad { get; set; }

    
    public decimal Precio { get; set; }


    [StringLength(DominioConstantes.OBSERVACIONESORDENITEMS_MAXIMO)]
    public string? Observaciones { get; set; }
}