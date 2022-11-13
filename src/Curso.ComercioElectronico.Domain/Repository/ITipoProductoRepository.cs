namespace Curso.ComercioElectronico.Domain;

public interface ITipoProductoRepository :  IRepository<TipoProducto,int> {


    Task<bool> ExisteUso(string uso);

    Task<bool> ExisteUso(string uso, int idExcluir);


}