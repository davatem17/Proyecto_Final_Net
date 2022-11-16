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

    public async Task<bool> DeleteAsync(Guid productoId)
    {
        //Reglas validaciones
        var producto = await productoRepository.GetByIdAsync(productoId);
        if (producto == null)
        {
            throw new ArgumentException($"El producto con el id: {productoId}, no existe");
        }
        productoRepository.Delete(producto);
        await productoRepository.UnitOfWork.SaveChangesAsync();
        return true;
    }

    public ListaPaginada<ProductoDto> GetAll(int limit = 10, int offset = 0)
    {
        //Lista.
        var consulta = productoRepository.GetAllIncluding(x => x.Marca, x => x.Bodega,
                                                        x => x.TipoProducto);
        //Ejecuatar linq. Total registros
        var total = consulta.Count();

        //Obtener el listado paginado..
        var consultaListaProductosDto = consulta.Skip(offset)
                                        .Take(limit)
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
                                                MarcaId = x.MarcaId,
                                                Marca = x.Marca.Nombre,
                                                TipoProductoId = x.TipoProductoId,
                                                TipoProducto = x.TipoProducto.Uso,
                                                BodegaId = x.BodegaId,
                                                Bodega = x.Bodega.Nombre
                                            }
                                        );
        var resultado = new ListaPaginada<ProductoDto>();
        resultado.Total = total;
        resultado.Lista = consultaListaProductosDto.ToList();
        return resultado;
    }

    public Task<ProductoDto> GetByIdAsync(Guid id)
    {
        var consulta = productoRepository.GetAllIncluding(x => x.Bodega, x => x.Marca, x => x.TipoProducto);
        consulta = consulta.Where(x => x.Id == id);

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

    public async Task<ListaPaginada<ProductoDto>> GetListAsync(ProductoListInput input)
    {
        var consulta = productoRepository.GetAllIncluding(x => x.Bodega, x => x.Marca, x => x.TipoProducto);
        //Aplicar filtros
        if (input.TipoProductoId.HasValue)
        {
            consulta = consulta.Where(x => x.TipoProductoId == input.TipoProductoId);
        }

        if (input.MarcaId.HasValue)
        {
            consulta = consulta.Where(x => x.MarcaId == input.MarcaId);
        }

        if (input.BodegaId.HasValue)
        {
            consulta = consulta.Where(x => x.BodegaId == input.BodegaId);
        }

        if (!string.IsNullOrEmpty(input.Nombre))
        {

            //consulta = consulta.Where(x => x.Nombre.Contains(input.ValorBuscar) ||
            //    x.Codigo.StartsWith(input.ValorBuscar));
            consulta = consulta.Where(x => x.Nombre.Contains(input.Nombre));
        }
        if (!string.IsNullOrEmpty(input.Observacion))
        {

            //consulta = consulta.Where(x => x.Nombre.Contains(input.ValorBuscar) ||
            //    x.Codigo.StartsWith(input.ValorBuscar));
            consulta = consulta.Where(x => x.Observaciones.Contains(input.Observacion));
        }

        //Ejecuatar linq. Total registros
        var total = consulta.Count();

        //Aplicar paginacion
        consulta = consulta.Skip(input.Offset)
                    .Take(input.Limit);

        //Obtener el listado paginado. (Proyeccion)
        var consultaListaProductosDto = consulta
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
        var resultado = new ListaPaginada<ProductoDto>();
        resultado.Total = total;
        resultado.Lista = consultaListaProductosDto.ToList();

        return resultado;
    }

    public async Task UpdateAsync(Guid id, ProductoCrearActualizarDto productoDto)
    {
        var producto = await productoRepository.GetByIdAsync(id);
        //Mapeo Dto => Entidad
        //Automatico
        producto = mapper.Map(productoDto, producto);
        //Persistencia objeto
        await productoRepository.UpdateAsync(producto);
        await productoRepository.UnitOfWork.SaveChangesAsync();
        //Mapeo Entidad => Dto
        //var productoCreado = mapper.Map<ProductoDto>(producto);
        return;
    }
}
