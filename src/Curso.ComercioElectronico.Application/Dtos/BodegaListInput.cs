namespace Curso.ComercioElectronico.Application;

public class BodegaListInput {

    public int Limit {get;set;} = 10;
    public int Offset {get;set;} = 0;

    public string? Nombre {get;set;}
    public string? Ubicacion {get;set;}

}