using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;



public class EditorialAppService : IEditorialAppService
{
    private readonly IEditorialRepository repository;
    //private readonly IUnitOfWork unitOfWork;

    public EditorialAppService(IEditorialRepository repository)
    {
        this.repository = repository;
        //this.unitOfWork = unitOfWork;
    }

    public async Task<EditorialDto> CreateAsync(EditorialCrearActualizarDto editorialDto)
    {
        
        //Reglas Validaciones... 
        var existeNombreEditorial = await repository.ExisteNombre(editorialDto.Nombre);
        if (existeNombreEditorial){
            throw new ArgumentException($"Ya existe un autor con el nombre {editorialDto.Nombre}");
        }
 
        //Mapeo Dto => Entidad
        var editorial = new Editorial();
        editorial.Nombre = editorialDto.Nombre;
        editorial.Ubicacion = editorialDto.Ubicacion;
 
        //Persistencia objeto
        editorial = await repository.AddAsync(editorial);
        //await unitOfWork.SaveChangesAsync();

        //Mapeo Entidad => Dto
        var editorialCreada = new EditorialDto();
        editorialCreada.Nombre = editorial.Nombre;
        editorialCreada.Ubicacion = editorial.Ubicacion;
        editorialCreada.Id = editorial.Id;

        //TODO: Enviar un correo electronica... 

        return editorialCreada;
    }

    public async Task UpdateAsync(int id, EditorialCrearActualizarDto editorialDto)
    {
        var editorial = await repository.GetByIdAsync(id);
        if (editorial == null){
            throw new ArgumentException($"La editorial con el id: {id}, no existe");
        }
        
        var existeNombreEditorial = await repository.ExisteNombre(editorialDto.Nombre,id);
        if (existeNombreEditorial){
            throw new ArgumentException($"Ya existe una editorial con el nombre {editorialDto.Nombre}");
        }

        //Mapeo Dto => Entidad
        editorial.Nombre = editorialDto.Nombre;
        editorial.Ubicacion = editorialDto.Ubicacion;
        

        //Persistencia objeto
        await repository.UpdateAsync(editorial);
        //await unitOfWork.SaveChangesAsync();

        return;
    }

    public async Task<bool> DeleteAsync(int editorialId)
    {
        //Reglas Validaciones... 
        var editorial = await repository.GetByIdAsync(editorialId);
        if (editorial == null){
            throw new ArgumentException($"La editorial con el id: {editorialId}, no existe");
        }

        repository.Delete(editorial);
        //await unitOfWork.SaveChangesAsync();

        return true;
    }

    public ICollection<EditorialDto> GetAll()
    {
        var editorialList = repository.GetAll();

        var editorialListDto =  from e in editorialList
                            select new EditorialDto(){
                                Id = e.Id,
                                Nombre = e.Nombre,
                                Ubicacion = e.Ubicacion
                            };

        return editorialListDto.ToList();
    }

    
}