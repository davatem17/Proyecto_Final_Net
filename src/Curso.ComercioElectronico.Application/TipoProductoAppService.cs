using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Curso.ComercioElectronico.Application;

public class TipoProductoAppService : ITipoProductoAppService
{
    
    private readonly ITipoProductoRepository tipoProductoRepository;
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    private readonly ILogger<TipoProductoAppService> logger;
    public TipoProductoAppService(ITipoProductoRepository tipoProductoRepository,
            ILogger<TipoProductoAppService> logger, IMapper mapper)
    {
        this.tipoProductoRepository = tipoProductoRepository;
        this.mapper = mapper;
        this.logger = logger;
    }
    
    public async Task<TipoProductoDto> CreateAsync(TipoProductoCrearActualizarDto tipoProductoDto)
    {
        logger.LogInformation("Crear Tipo Producto");

        //Automatico
        var tipoProducto = mapper.Map<TipoProducto>(tipoProductoDto);

        //Persistencia objeto
        tipoProducto = await tipoProductoRepository.AddAsync(tipoProducto);
        await tipoProductoRepository.UnitOfWork.SaveChangesAsync();

        //Mapeo Entidad => Dto
        var tipoProductoCreado = mapper.Map<TipoProductoDto>(tipoProducto);
        return tipoProductoCreado;
    }

    public async Task<bool> DeleteAsync(int tipoProductoId)
    {
        //Reglas Validaciones... 
        var tipoProducto = await tipoProductoRepository.GetByIdAsync(tipoProductoId);
        if (tipoProducto == null)
        {
            throw new ArgumentException($"El tipo producto con el id: {tipoProductoId}, no existe");
        }

        tipoProductoRepository.Delete(tipoProducto);
        await tipoProductoRepository.UnitOfWork.SaveChangesAsync();

        return true;
    }
    
    public ICollection<TipoProductoDto> GetAll()
    {
        var tipoProductoList = tipoProductoRepository.GetAll();

        var tipoProductoListDto = from t in tipoProductoList
                                  select new TipoProductoDto()
                                  {
                                      Id = t.Id,
                                      Uso = t.Uso,
                                      Flamable = t.Flamable,
                                      Fragil = t.Fragil,
                                      Toxico = t.Toxico
                                  };

        return tipoProductoListDto.ToList();
    }
    

    
    public async Task UpdateAsync(int id, TipoProductoCrearActualizarDto tipoProductoDto)
    {
        var tipoProducto = await tipoProductoRepository.GetByIdAsync(id);


        if (tipoProducto == null)
        {
            throw new ArgumentException($"El tipo producto con el id: {id}, no existe");
        }

        var existeUsoTipoProducto = await tipoProductoRepository.ExisteUso(tipoProductoDto.Uso, id);
        if (existeUsoTipoProducto)
        {
            throw new ArgumentException($"Ya existe un tipo producto con el uso {tipoProductoDto.Uso}");
        }

        //Mapeo Dto => Entidad
        tipoProducto = mapper.Map(tipoProductoDto, tipoProducto);

        //Persistencia objeto
        await tipoProductoRepository.UpdateAsync(tipoProducto);
        await tipoProductoRepository.UnitOfWork.SaveChangesAsync();

        return;
    }
    
}

