namespace Curso.ComercioElectronico.Domain;

public interface IBodegaRepository :  IRepository<Bodega,int> {


    Task<bool> ExisteNombre(string nombre);

    Task<bool> ExisteNombre(string nombre, int idExcluir);


}