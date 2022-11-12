

using Curso.ComercioElectronico.Application;
using Microsoft.AspNetCore.Mvc;

namespace Curso.ComercioElectronico.HttpApi.Controllers;


[ApiController]
[Route("[controller]")]
public class LibroController : ControllerBase
{

    private readonly ILibroAppService libroAppService;

    public LibroController(ILibroAppService libroAppService)
    {
        this.libroAppService = libroAppService;
    }

    [HttpGet]
    public ICollection<LibroDto> GetAll()
    {

        return libroAppService.GetAll();
    }

    [HttpPost]
    public async Task<LibroDto> CreateAsync(LibroCrearActualizarDto libro)
    {

        return await libroAppService.CreateAsync(libro);

    }

    [HttpPut]
    public async Task UpdateAsync(int id, LibroCrearActualizarDto libro)
    {

        await libroAppService.UpdateAsync(id, libro);

    }

    [HttpDelete]
    public async Task<bool> DeleteAsync(int libroId)
    {

        return await libroAppService.DeleteAsync(libroId);

    }

}