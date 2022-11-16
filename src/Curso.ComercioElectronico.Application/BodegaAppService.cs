using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Curso.ComercioElectronico.Application;

public class BodegaAppService : IBodegaAppService
{
    private readonly IBodegaRepository bodegaRepository;
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    private readonly ILogger<BodegaAppService> logger;
    public BodegaAppService(IBodegaRepository bodegaRepository,

        ILogger<BodegaAppService> logger, IMapper mapper)
    {
        this.bodegaRepository = bodegaRepository;
        this.mapper = mapper;
        this.logger = logger;
    }
    public async Task<BodegaDto> CreateAsync(BodegaCrearActualizarDto bodegaDto)
    {
        logger.LogInformation("Crear Bodega");
        //Mapeo Dto => Entidad. (Manual)

        //Automatico
        var bodega = mapper.Map<Bodega>(bodegaDto);
        //Persistencia objeto
        bodega = await bodegaRepository.AddAsync(bodega);
        await bodegaRepository.UnitOfWork.SaveChangesAsync();
        //Mapeo Entidad => Dto

        var bodegaCreada = mapper.Map<BodegaDto>(bodega);
        return bodegaCreada;
    }

    public async Task<bool> DeleteAsync(int bodegaId)
    {
        //Reglas Validaciones... 
        var bodega = await bodegaRepository.GetByIdAsync(bodegaId);
        if (bodega == null)
        {
            throw new ArgumentException($"La bodega con el id: {bodegaId}, no existe");
        }

        bodegaRepository.Delete(bodega);
        await bodegaRepository.UnitOfWork.SaveChangesAsync();

        return true;
    }

    public ICollection<BodegaDto> GetAll()
    {
        var bodegaList = bodegaRepository.GetAll();

        var bodegaListDto = from b in bodegaList
                            select new BodegaDto()
                            {
                                Id = b.Id,
                                Nombre = b.Nombre,
                                Ubicacion = b.Ubicacion,
                                Telefono = b.Telefono
                            };

        return bodegaListDto.ToList();
    }

    public async Task UpdateAsync(int id, BodegaCrearActualizarDto bodegaDto)
    {
        var bodega = await bodegaRepository.GetByIdAsync(id);


        if (bodega == null)
        {
            throw new ArgumentException($"La bodega con el id: {id}, no existe");
        }

        var existeNombreBodega = await bodegaRepository.ExisteNombre(bodegaDto.Nombre, id);
        if (existeNombreBodega)
        {
            throw new ArgumentException($"Ya existe una bodega con el nombre {bodegaDto.Nombre}");
        }

        //Mapeo Dto => Entidad
        bodega = mapper.Map(bodegaDto, bodega);

        //Persistencia objeto
        await bodegaRepository.UpdateAsync(bodega);
        await bodegaRepository.UnitOfWork.SaveChangesAsync();

        return;
    }
    public async Task<ListaPaginada<BodegaDto>> GetListAsync(BodegaListInput input)
    {
        var consulta = bodegaRepository.GetAllIncluding();


        if (!string.IsNullOrEmpty(input.Nombre))
        {

            //consulta = consulta.Where(x => x.Nombre.Contains(input.ValorBuscar) ||
            //    x.Codigo.StartsWith(input.ValorBuscar));
            consulta = consulta.Where(x => x.Nombre.Contains(input.Nombre));
        }
        if (!string.IsNullOrEmpty(input.Ubicacion))
        {

            //consulta = consulta.Where(x => x.Nombre.Contains(input.ValorBuscar) ||
            //    x.Codigo.StartsWith(input.ValorBuscar));
            consulta = consulta.Where(x => x.Ubicacion.Contains(input.Ubicacion));
        }

        //Ejecuatar linq. Total registros
        var total = consulta.Count();

        //Aplicar paginacion
        consulta = consulta.Skip(input.Offset)
                    .Take(input.Limit);

        //Obtener el listado paginado. (Proyeccion)
        var consultaListaBodegaDto = consulta
                                        .Select(
                                            b => new BodegaDto()
                                            {
                                                Id = b.Id,
                                                Nombre = b.Nombre,
                                                Ubicacion = b.Ubicacion,
                                                Telefono = b.Telefono
                                            }
                                        );
        var resultado = new ListaPaginada<BodegaDto>();
        resultado.Total = total;
        resultado.Lista = consultaListaBodegaDto.ToList();

        return resultado;
    }
}