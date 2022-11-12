using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;


public interface ILibroAppService
{

    ICollection<LibroDto> GetAll();

    Task<LibroDto> CreateAsync(LibroCrearActualizarDto marca);

    Task UpdateAsync (int id, LibroCrearActualizarDto marca);

    Task<bool> DeleteAsync(int marcaId);
}
 
 