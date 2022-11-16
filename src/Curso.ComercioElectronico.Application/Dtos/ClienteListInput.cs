namespace Curso.ComercioElectronico.Application;

public class ClienteListInput {

    public int Limit {get;set;} = 10;
    public int Offset {get;set;} = 0;

    

    public string? BuscarNombre {get;set;}

    public string? BuscarApellido {get;set;}

    public string? BuscarCedula {get;set;}

}