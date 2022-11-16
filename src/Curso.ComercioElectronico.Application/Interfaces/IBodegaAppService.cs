using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;


public interface IBodegaAppService
{

    //Task<MarcaDto> GetByIdAsync(int id);
    ICollection<BodegaDto> GetAll();

    Task<BodegaDto> CreateAsync(BodegaCrearActualizarDto bodega);

    Task UpdateAsync (int id, BodegaCrearActualizarDto bodega);

    Task<bool> DeleteAsync(int bodegaId);
    Task<ListaPaginada<BodegaDto>> GetListAsync(BodegaListInput input);
}
 
 
