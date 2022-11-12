using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;


public interface IAutorAppService
{

    ICollection<AutorDto> GetAll();

    Task<AutorDto> CreateAsync(AutorCrearActualizarDto autor);

    Task UpdateAsync (int id, AutorCrearActualizarDto autor);

    Task<bool> DeleteAsync(int autorId);
}
 
 