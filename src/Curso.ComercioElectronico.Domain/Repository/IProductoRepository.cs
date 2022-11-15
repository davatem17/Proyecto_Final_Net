namespace Curso.ComercioElectronico.Domain;

public interface IProductoRepository :  IRepository<Producto,Guid> {


    Task<bool> ExisteNombre(string nombre);

    Task<bool> ExisteNombre(string nombre, Guid idExcluir);


}