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
        CreateMap<TipoProductoCrearActualizarDto, TipoProducto>();
        CreateMap<TipoProducto, TipoProductoDto>();
        CreateMap<BodegaCrearActualizarDto, Bodega>();
        CreateMap<Bodega, BodegaDto>();
        CreateMap<ProductoCrearActualizarDto, Producto>();
        CreateMap<Producto, ProductoDto>();
        CreateMap<ClienteCrearActualizarDto, Cliente>();
        CreateMap<Cliente, ClienteDto>();
        //TODO: Agregar otros mapeos que se requieren...

    }
}