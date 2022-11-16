namespace Curso.ComercioElectronico.Application;

public class MarcaListInput {

    public int Limit {get;set;} = 10;
    public int Offset {get;set;} = 0;

    public string? BuscarNombre {get;set;}
    public string? BuscarPaisOrigen {get;set;}

}