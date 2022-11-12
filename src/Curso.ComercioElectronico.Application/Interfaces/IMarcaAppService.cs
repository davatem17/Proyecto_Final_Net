using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;


public interface IMarcaAppService
{

    //Task<MarcaDto> GetByIdAsync(int id);
    ICollection<MarcaDto> GetAll();

    Task<MarcaDto> CreateAsync(MarcaCrearActualizarDto marca);

    Task UpdateAsync (int id, MarcaCrearActualizarDto marca);

    Task<bool> DeleteAsync(int marcaId);
}
 
 
