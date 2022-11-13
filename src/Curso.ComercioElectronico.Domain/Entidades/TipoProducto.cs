using System.ComponentModel.DataAnnotations;

namespace Curso.ComercioElectronico.Domain;


public class TipoProducto
{
    [Required]
    public int Id { get; set; }

    [Required]
    [StringLength(DominioConstantes.USO_MAXIMO)]
    public string Uso { get; set; }

    [Required]
    public bool Flamable { get; set; }

    [Required]
    public bool Fragil { get; set; }

    [Required]
    public bool Toxico { get; set; }

}