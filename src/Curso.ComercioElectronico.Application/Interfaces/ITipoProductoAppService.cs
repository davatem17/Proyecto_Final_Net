using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;
namespace Curso.ComercioElectronico.Application;

public interface ITipoProductoAppService
{
    //Task<TipoProductoDto> GetByIdAsync(int id);

    ICollection<TipoProductoDto> GetAll();

    Task<TipoProductoDto> CreateAsync(TipoProductoCrearActualizarDto tipoProducto);

    Task UpdateAsync (int id, TipoProductoCrearActualizarDto tipoProducto);

    Task<bool> DeleteAsync(int tipoProductoId);
}