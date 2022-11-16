using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Curso.ComercioElectronico.Application;

public class OrdenAppService : IOrdenAppService
{
    private readonly IOrdenRepository ordenRepository;
    private readonly IProductoRepository productoRepository;
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;

    private readonly ILogger<OrdenAppService> logger;

    public OrdenAppService(IOrdenRepository ordenRepository, IProductoRepository productoRepository,

        ILogger<OrdenAppService> logger, IMapper mapper)
    {
        this.ordenRepository = ordenRepository;
        this.productoRepository = productoRepository;
        this.mapper = mapper;
        this.logger = logger;
    }
    public async Task<OrdenDto> CreateAsync(OrdenCrearDto ordenDto)
    {
        logger.LogInformation("Crear Orden");
        //Mapeo Dto => Entidad
        //Automatico
        var orden = mapper.Map<Orden>(ordenDto);
        //Persistencia objeto
        orden = await ordenRepository.AddAsync(orden);
        await ordenRepository.UnitOfWork.SaveChangesAsync();
        //Mapeo Entidad => Dto
        //var productoCreado = mapper.Map<ProductoDto>(producto);
        return await GetByIdAsync(orden.Id);
    }
    public Task<OrdenDto> GetByIdAsync(Guid id)
    {
        var consulta = ordenRepository.GetAllIncluding(x => x.Cliente, x => x.Items); 
        consulta = consulta.Where(x => x.Id == id);
        var consultaOrdenDto = consulta
                                .Select(
                                    x => new OrdenDto()
                                    {
                                         //VendedorNombre = $"{x.Vendedor.Nombre} {x.Vendedor.Apellido}", 
                                         Id = x.Id,
                                         Cliente = x.Cliente.Nombre,
                                         ClienteId = x.ClienteId,
                                         Estado = x.Estado,
                                         Fecha = x.Fecha,
                                         FechaAnulacion = x.FechaAnulacion,
                                         Observaciones = x.Observaciones,
                                         Total = x.Total,
                                        Items = (ICollection<OrdenItem>)x.Items.Select(item => new OrdenItemDto(){
                                            Cantidad = item.Cantidad,
                                            Id = item.Id,
                                            Observaciones = item.Observaciones,
                                            OrdenId = item.OrdenId,
                                            Precio  = item.Precio,
                                            ProductoId = item.ProductoId,
                                            Producto = item.Producto.Nombre
                                         }).ToList()
                                    }
                                ); 
        return Task.FromResult(consultaOrdenDto.SingleOrDefault());
    }
}