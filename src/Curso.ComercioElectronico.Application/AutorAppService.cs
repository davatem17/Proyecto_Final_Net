using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;



public class AutorAppService : IAutorAppService
{
    private readonly IAutorRepository repository;
    //private readonly IUnitOfWork unitOfWork;

    public AutorAppService(IAutorRepository repository)
    {
        this.repository = repository;
        //this.unitOfWork = unitOfWork;
    }

    public async Task<AutorDto> CreateAsync(AutorCrearActualizarDto autorDto)
    {
        
        //Reglas Validaciones... 
        var existeNombreAutor = await repository.ExisteNombre(autorDto.Nombre);
        if (existeNombreAutor){
            throw new ArgumentException($"Ya existe un autor con el nombre {autorDto.Nombre}");
        }
 
        //Mapeo Dto => Entidad
        var autor = new Autor();
        autor.Nombre = autorDto.Nombre;
        autor.Domicilio = autorDto.Domicilio;
        autor.Edad = autorDto.Edad;
 
        //Persistencia objeto
        autor = await repository.AddAsync(autor);
        //await unitOfWork.SaveChangesAsync();

        //Mapeo Entidad => Dto
        var autorCreada = new AutorDto();
        autorCreada.Nombre = autor.Nombre;
        autorCreada.Domicilio = autor.Domicilio;
        autorCreada.Edad = autor.Edad;
        autorCreada.Id = autor.Id;

        //TODO: Enviar un correo electronica... 

        return autorCreada;
    }

    public async Task UpdateAsync(int id, AutorCrearActualizarDto autorDto)
    {
        var autor = await repository.GetByIdAsync(id);
        if (autor == null){
            throw new ArgumentException($"El autor con el id: {id}, no existe");
        }
        
        var existeNombreAutor = await repository.ExisteNombre(autorDto.Nombre,id);
        if (existeNombreAutor){
            throw new ArgumentException($"Ya existe un autor con el nombre {autorDto.Nombre}");
        }

        //Mapeo Dto => Entidad
        autor.Nombre = autorDto.Nombre;
        autor.Domicilio = autorDto.Domicilio;
        autor.Edad = autorDto.Edad;

        //Persistencia objeto
        await repository.UpdateAsync(autor);
        //await unitOfWork.SaveChangesAsync();

        return;
    }

    public async Task<bool> DeleteAsync(int autorId)
    {
        //Reglas Validaciones... 
        var autor = await repository.GetByIdAsync(autorId);
        if (autor == null){
            throw new ArgumentException($"El autor con el id: {autorId}, no existe");
        }

        repository.Delete(autor);
        //await unitOfWork.SaveChangesAsync();

        return true;
    }

    public ICollection<AutorDto> GetAll()
    {
        var autorList = repository.GetAll();

        var autorListDto =  from a in autorList
                            select new AutorDto(){
                                Id = a.Id,
                                Nombre = a.Nombre,
                                Domicilio = a.Domicilio,
                                Edad = a.Edad
                            };

        return autorListDto.ToList();
    }

    
}