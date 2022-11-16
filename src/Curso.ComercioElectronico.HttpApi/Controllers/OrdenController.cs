

using Curso.ComercioElectronico.Application;
using Microsoft.AspNetCore.Mvc;

namespace Curso.ComercioElectronico.HttpApi.Controllers;


[ApiController]
[Route("[controller]")]
public class OrdenController : ControllerBase
{

    private readonly IOrdenAppService ordenAppService;

    public OrdenController(IOrdenAppService ordenAppService)
    {
        this.ordenAppService = ordenAppService;
    }

 

    [HttpGet("{id}")]
    public async Task<OrdenDto>  GetByIdAsync(Guid id)
    {
        return await ordenAppService.GetByIdAsync(id);
    }


    

    [HttpPost]
    public async Task<OrdenDto> CreateAsync(OrdenCrearDto marca)
    {

        return await ordenAppService.CreateAsync(marca);

    }


}