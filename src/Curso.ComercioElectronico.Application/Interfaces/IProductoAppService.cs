using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;


public interface IProductoAppService
{

    Task<ProductoDto> GetByIdAsync(Guid id);

    //Permitir filtrar marca,tipo producto, y por texto (nombre,codigo). Paginacion.
    ListaPaginada<ProductoDto> GetAll(int limit=10,int offset=0);

/*     ListaPaginada<ProductoDto> GetList(int limit=10,int offset=0,string? tipoProductoId="",
                        string? marcaId="",string? valorBuscar=""); */

    Task<ListaPaginada<ProductoDto>> GetListAsync(ProductoListInput input);


    Task<ProductoDto> CreateAsync(ProductoCrearActualizarDto producto);

    Task UpdateAsync (Guid id, ProductoCrearActualizarDto producto);

    Task<bool> DeleteAsync(Guid marcaId);
}
 