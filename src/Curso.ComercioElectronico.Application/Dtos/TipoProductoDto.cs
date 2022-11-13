using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;

public class TipoProductoDto
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
