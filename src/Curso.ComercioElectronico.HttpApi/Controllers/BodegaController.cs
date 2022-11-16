using Curso.ComercioElectronico.Application;
using Microsoft.AspNetCore.Mvc;

namespace Curso.ComercioElectronico.HttpApi.Controllers;


[ApiController]
[Route("[controller]")]
public class BodegaController : ControllerBase
{

    private readonly IBodegaAppService bodegaAppService;

    public BodegaController(IBodegaAppService bodegaAppService)
    {
        this.bodegaAppService = bodegaAppService;
    }

    [HttpGet]
    public ICollection<BodegaDto> GetAll()
    {

        return bodegaAppService.GetAll();
    }

    [HttpPost]
    public async Task<BodegaDto> CreateAsync(BodegaCrearActualizarDto bodega)
    {

        return await bodegaAppService.CreateAsync(bodega);

    }

    [HttpPut]
    public async Task UpdateAsync(int id, BodegaCrearActualizarDto bodega)
    {

        await bodegaAppService.UpdateAsync(id, bodega);

    }

    [HttpDelete]
    public async Task<bool> DeleteAsync(int bodegaId)
    {

        return await bodegaAppService.DeleteAsync(bodegaId);

    }
    [HttpGet("list")]
    public  Task<ListaPaginada<BodegaDto>> GetListAsync([FromQuery]BodegaListInput input)
    {
        return bodegaAppService.GetListAsync(input);
    }

}