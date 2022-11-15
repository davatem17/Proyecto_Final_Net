using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Curso.ComercioElectronico.Application;

public class ProductoAppService : IProductoAppService
{
    private readonly IProductoRepository productoRepository;
    private readonly IMarcaRepository marcaRepository;
    private readonly ITipoProductoRepository tipoProductoRepository;
    private readonly IBodegaRepository bodegaRepository;
    private readonly IMapper mapper;
    private readonly ILogger<ProductoAppService> logger;
    public ProductoAppService(IProductoRepository productoRepository, ITipoProductoRepository tipoProductoRepository,
                            IMarcaRepository marcaRepository, IBodegaRepository bodegaRepository,
                            ILogger<ProductoAppService> logger, IMapper mapper)
    {
        this.productoRepository = productoRepository;
        this.tipoProductoRepository = tipoProductoRepository;
        this.marcaRepository = marcaRepository;
        this.bodegaRepository = bodegaRepository;
        this.logger = logger;
        this.mapper = mapper;
    }

    public async Task<ProductoDto> CreateAsync(ProductoCrearActualizarDto productoDto)
    {
        logger.LogInformation("Crear Producto");
        //Mapeo Dto => Entidad
        //Automatico
        var producto = mapper.Map<Producto>(productoDto);
        //Persistencia objeto
        producto = await productoRepository.AddAsync(producto);
        await productoRepository.UnitOfWork.SaveChangesAsync();
        //Mapeo Entidad => Dto
        //var productoCreado = mapper.Map<ProductoDto>(producto);
        return await GetByIdAsync(producto.Id);
    }

    public Task<bool> DeleteAsync(Guid marcaId)
    {
        throw new NotImplementedException();
    }

    public ListaPaginada<ProductoDto> GetAll(int limit = 10, int offset = 0)
    {
        throw new NotImplementedException();
    }

    public Task<ProductoDto> GetByIdAsync(Guid id)
    {
        var consulta = productoRepository.GetAllIncluding(x => x.Bodega, x => x.Marca, x => x.TipoProducto);
        consulta = consulta.Where(x=>x.Id == id);

        var consultaProductoDto = consulta
                                    .Select(
                                        x => new ProductoDto()
                                        {
                                            Id = x.Id,
                                            Nombre = x.Nombre,
                                            Precio = x.Precio,
                                            Observaciones = x.Observaciones,
                                            Caducidad = x.Caducidad,
                                            FechaElaboracion = x.FechaElaboracion,
                                            Disponible = x.Disponible,
                                            BodegaId = x.BodegaId,
                                            Bodega = x.Bodega.Nombre,
                                            TipoProductoId = x.TipoProductoId,
                                            TipoProducto = x.TipoProducto.Uso,
                                            MarcaId = x.MarcaId,
                                            Marca = x.Marca.Nombre
                                        }
                                    );
            return Task.FromResult(consultaProductoDto.SingleOrDefault());
    }

    public Task<ListaPaginada<ProductoDto>> GetListAsync(ProductoListInput input)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Guid id, ProductoCrearActualizarDto producto)
    {
        throw new NotImplementedException();
    }
}