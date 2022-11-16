using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Curso.ComercioElectronico.Application;

public class MarcaAppService : IMarcaAppService
{
    private readonly IMarcaRepository marcaRepository;
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;

    private readonly ILogger<MarcaAppService> logger;

    public MarcaAppService(IMarcaRepository marcaRepository,

        ILogger<MarcaAppService> logger, IMapper mapper)
    {
        this.marcaRepository = marcaRepository;
        this.mapper = mapper;
        this.logger = logger;
    }

    public async Task<MarcaDto> CreateAsync(MarcaCrearActualizarDto marcaDto)
    {

        logger.LogInformation("Crear Marca");
        //Mapeo Dto => Entidad. (Manual)
        //var marca = new Marca();
        //tipoProducto.Nombre = tipoProductoDto.Nombre;

        //Automatico
        var marca = mapper.Map<Marca>(marcaDto);
        //Persistencia objeto
        marca = await marcaRepository.AddAsync(marca);
        await marcaRepository.UnitOfWork.SaveChangesAsync();
        //Mapeo Entidad => Dto
        //var marcaCreada = new MarcaDto();
        //marcaCreada.Nombre = marca.Nombre;
        //marcaCreada.Id = marca.Id;
        var marcaCreada = mapper.Map<MarcaDto>(marca);
        return marcaCreada;
    }

    public async Task UpdateAsync(int id, MarcaCrearActualizarDto marcaDto)
    {
        var marca = await marcaRepository.GetByIdAsync(id);


        if (marca == null)
        {
            throw new ArgumentException($"La marca con el id: {id}, no existe");
        }

        var existeNombreMarca = await marcaRepository.ExisteNombre(marcaDto.Nombre, id);
        if (existeNombreMarca)
        {
            throw new ArgumentException($"Ya existe una marca con el nombre {marcaDto.Nombre}");
        }

        //Mapeo Dto => Entidad
        marca = mapper.Map(marcaDto, marca);

        //Persistencia objeto
        await marcaRepository.UpdateAsync(marca);
        await marcaRepository.UnitOfWork.SaveChangesAsync();



        return;
    }

    public async Task<bool> DeleteAsync(int marcaId)
    {
        //Reglas Validaciones... 
        var marca = await marcaRepository.GetByIdAsync(marcaId);
        if (marca == null)
        {
            throw new ArgumentException($"La marca con el id: {marcaId}, no existe");
        }

        marcaRepository.Delete(marca);
        await marcaRepository.UnitOfWork.SaveChangesAsync();

        return true;
    }

    public ICollection<MarcaDto> GetAll()
    {
        var marcaList = marcaRepository.GetAll();

        var marcaListDto = from m in marcaList
                           select new MarcaDto()
                           {
                               Id = m.Id,
                               Nombre = m.Nombre,
                               PaisOrigen = m.PaisOrigen,
                               PresenciaInternacional = m.PresenciaInternacional
                           };

        return marcaListDto.ToList();
    }

    public async Task<ListaPaginada<MarcaDto>> GetListAsync(MarcaListInput input)
    {
        var consulta = marcaRepository.GetAllIncluding();


        if (!string.IsNullOrEmpty(input.BuscarNombre))
        {

            //consulta = consulta.Where(x => x.Nombre.Contains(input.ValorBuscar) ||
            //    x.Codigo.StartsWith(input.ValorBuscar));
            consulta = consulta.Where(x => x.Nombre.Contains(input.BuscarNombre));
        }
        if (!string.IsNullOrEmpty(input.BuscarPaisOrigen))
        {

            //consulta = consulta.Where(x => x.Nombre.Contains(input.ValorBuscar) ||
            //    x.Codigo.StartsWith(input.ValorBuscar));
            consulta = consulta.Where(x => x.PaisOrigen.Contains(input.BuscarPaisOrigen));
        }

        //Ejecuatar linq. Total registros
        var total = consulta.Count();

        //Aplicar paginacion
        consulta = consulta.Skip(input.Offset)
                    .Take(input.Limit);

        //Obtener el listado paginado. (Proyeccion)
        var consultaListaMarcaDto = consulta
                                        .Select(
                                            m => new MarcaDto()
                                            {
                                                Id = m.Id,
                                                Nombre = m.Nombre,
                                                PaisOrigen = m.PaisOrigen,
                                                PresenciaInternacional = m.PresenciaInternacional
                                            }
                                        );
        var resultado = new ListaPaginada<MarcaDto>();
        resultado.Total = total;
        resultado.Lista = consultaListaMarcaDto.ToList();

        return resultado;
    }


}
