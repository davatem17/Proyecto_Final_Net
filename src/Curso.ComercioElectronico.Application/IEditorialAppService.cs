using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;


public interface IEditorialAppService
{

    ICollection<EditorialDto> GetAll();

    Task<EditorialDto> CreateAsync(EditorialCrearActualizarDto marca);

    Task UpdateAsync (int id, EditorialCrearActualizarDto marca);

    Task<bool> DeleteAsync(int marcaId);
}
 
 