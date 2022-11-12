using AutoMapper;
using Curso.ComercioElectronico.Application;
using Curso.ComercioElectronico.Domain;
namespace Curso.ComercioElectronico.Application;

public class ConfiguracionesMapeoProfile : Profile
{
    //TipoProductoCrearActualizarDto => TipoProducto
    //TipoProducto => TipoProductoDto
    public ConfiguracionesMapeoProfile()
    {
        
        CreateMap<MarcaCrearActualizarDto, Marca>();
        CreateMap<Marca, MarcaDto>();
        //TODO: Agregar otros mapeos que se requieren...

    }
}