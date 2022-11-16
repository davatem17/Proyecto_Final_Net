using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;


public interface IClienteAppService
{

    ICollection<ClienteDto> GetAll();

    Task<ClienteDto> CreateAsync(ClienteCrearActualizarDto cliente);

    Task UpdateAsync(Guid clienteId, ClienteCrearActualizarDto clienteDto);

    Task<bool> DeleteAsync(Guid clienteId);
}
