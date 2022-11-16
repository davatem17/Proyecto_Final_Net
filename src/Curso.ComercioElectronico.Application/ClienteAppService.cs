using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Curso.ComercioElectronico.Application;

public class ClienteAppService : IClienteAppService
{
    private readonly IClienteRepository clienteRepository;
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;

    private readonly ILogger<ClienteAppService> logger;

    public ClienteAppService(IClienteRepository clienteRepository,

        ILogger<ClienteAppService> logger, IMapper mapper)
    {
        this.clienteRepository = clienteRepository;
        this.mapper = mapper;
        this.logger = logger;
    }

    public async Task<ClienteDto> CreateAsync(ClienteCrearActualizarDto clienteDto)
    {

        logger.LogInformation("Crear Cliente");
        //Mapeo Dto => Entidad. (Manual)
        //var marca = new Marca();
        //tipoProducto.Nombre = tipoProductoDto.Nombre;

        //Automatico
        var cliente = mapper.Map<Cliente>(clienteDto);
        //Persistencia objeto
        cliente = await clienteRepository.AddAsync(cliente);
        await clienteRepository.UnitOfWork.SaveChangesAsync();
        //Mapeo Entidad => Dto
        //var marcaCreada = new MarcaDto();
        //marcaCreada.Nombre = marca.Nombre;
        //marcaCreada.Id = marca.Id;
        var clienteCreado = mapper.Map<ClienteDto>(cliente);
        return clienteCreado;
    }

    public ICollection<ClienteDto> GetAll()
    {
        var clienteList = clienteRepository.GetAll();
        var clienteListDto = from c in clienteList
                             select new ClienteDto()
                             {
                                 Id = c.Id,
                                 Nombre = c.Nombre,
                                 Apellido = c.Apellido,
                                 Cedula = c.Cedula,
                                 CorreoElectronico = c.CorreoElectronico,
                                 Direccion = c.Direccion,
                                 Telefono = c.Telefono
                             };
        return clienteListDto.ToList();
    }

    public async Task UpdateAsync(Guid id, ClienteCrearActualizarDto clienteDto)
    {
        var cliente = await clienteRepository.GetByIdAsync(id);


        if (cliente == null)
        {
            throw new ArgumentException($"El cliente con el id: {id}, no existe");
        }

        var existeNombreCliente = await clienteRepository.ExisteNombre(clienteDto.Nombre, id);
        if (existeNombreCliente)
        {
            throw new ArgumentException($"Ya existe un cliente con el nombre {clienteDto.Nombre}");
        }

        //Mapeo Dto => Entidad
        cliente = mapper.Map(clienteDto, cliente);

        //Persistencia objeto
        await clienteRepository.UpdateAsync(cliente);
        await clienteRepository.UnitOfWork.SaveChangesAsync();



        return;
    }

    public async Task<bool> DeleteAsync(Guid clienteId)
    {
        //Reglas Validaciones... 
        var cliente = await clienteRepository.GetByIdAsync(clienteId);
        if (cliente == null)
        {
            throw new ArgumentException($"El cliente con el id: {clienteId}, no existe");
        }

        clienteRepository.Delete(cliente);
        await clienteRepository.UnitOfWork.SaveChangesAsync();

        return true;
    }
    public async Task<ListaPaginada<ClienteDto>> GetListAsync(ClienteListInput input)
    {
        var consulta = clienteRepository.GetAllIncluding();


        if (!string.IsNullOrEmpty(input.BuscarNombre))
        {

            //consulta = consulta.Where(x => x.Nombre.Contains(input.ValorBuscar) ||
            //    x.Codigo.StartsWith(input.ValorBuscar));
            consulta = consulta.Where(x => x.Nombre.Contains(input.BuscarNombre));
        }
        if (!string.IsNullOrEmpty(input.BuscarApellido))
        {

            //consulta = consulta.Where(x => x.Nombre.Contains(input.ValorBuscar) ||
            //    x.Codigo.StartsWith(input.ValorBuscar));
            consulta = consulta.Where(x => x.Apellido.Contains(input.BuscarApellido));
        }
        if (!string.IsNullOrEmpty(input.BuscarCedula))
        {

            //consulta = consulta.Where(x => x.Nombre.Contains(input.ValorBuscar) ||
            //    x.Codigo.StartsWith(input.ValorBuscar));
            consulta = consulta.Where(x => x.Cedula.Contains(input.BuscarCedula));
        }

        //Ejecuatar linq. Total registros
        var total = consulta.Count();

        //Aplicar paginacion
        consulta = consulta.Skip(input.Offset)
                    .Take(input.Limit);

        //Obtener el listado paginado. (Proyeccion)
        var consultaListaClientesDto = consulta
                                        .Select(
                                            c => new ClienteDto()
                                            {
                                                Id = c.Id,
                                                Nombre = c.Nombre,
                                                Apellido = c.Apellido,
                                                Cedula = c.Cedula,
                                                CorreoElectronico = c.CorreoElectronico,
                                                Direccion = c.Direccion,
                                                Telefono = c.Telefono
                                            }
                                        );
        var resultado = new ListaPaginada<ClienteDto>();
        resultado.Total = total;
        resultado.Lista = consultaListaClientesDto.ToList();

        return resultado;
    }

}
