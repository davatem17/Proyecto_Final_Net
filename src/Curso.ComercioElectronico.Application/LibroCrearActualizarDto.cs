using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;

  
public class LibroCrearActualizarDto
{
   
    [Required]
    [StringLength(DominioConstantes.NOMBRE_MAXIMO)]
    public string Nombre {get;set;}

    public DateTime FechaPublicacion {get;set;}

    [Required]
    public int AutorId {get;set;}
    

    [Required]
    public int EditorialId {get;set;}
    
    
}