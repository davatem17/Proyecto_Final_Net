using Curso.ComercioElectronico.Domain;
using System.ComponentModel.DataAnnotations;
namespace Curso.ComercioElectronico.Application;

public interface IOrdenAppService
{
    Task<OrdenDto> CreateAsync(OrdenCrearDto orden);
    Task<OrdenDto> GetByIdAsync(Guid id);

}