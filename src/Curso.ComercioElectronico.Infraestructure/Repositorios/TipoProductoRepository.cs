
using Curso.ComercioElectronico.Domain;
using Microsoft.EntityFrameworkCore;

namespace Curso.ComercioElectronico.Infraestructure;

public class TipoProductoRepository : EfRepository<TipoProducto,int>, ITipoProductoRepository
{
    public TipoProductoRepository(ComercioElectronicoDbContext context) : base(context)
    {
    }

    public async Task<bool> ExisteUso(string uso) {

        var resultado = await this._context.Set<TipoProducto>()
                       .AnyAsync(x => x.Uso.ToUpper() == uso.ToUpper());

        return resultado;
    }

    public async Task<bool> ExisteUso(string uso, int idExcluir)  {

        var query =  this._context.Set<TipoProducto>()
                       .Where(x => x.Id != idExcluir)
                       .Where(x => x.Uso.ToUpper() == uso.ToUpper())
                       ;

        var resultado = await query.AnyAsync();

        return resultado;
    }

    
}
