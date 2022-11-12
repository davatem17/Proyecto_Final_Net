using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;



public class LibroAppService : ILibroAppService
{
    private readonly ILibroRepository repository;
    //private readonly IUnitOfWork unitOfWork;

    public LibroAppService(ILibroRepository libroRepository)
    {
        this.repository = libroRepository;
    }
   

    public async Task<LibroDto> CreateAsync(LibroCrearActualizarDto libroDto)
    {
        
        //Reglas Validaciones... 
        var existeNombreLibro = await repository.ExisteNombre(libroDto.Nombre);
        if (existeNombreLibro){
            throw new ArgumentException($"Ya existe un libro con el nombre {libroDto.Nombre}");
        }
 
        //Mapeo Dto => Entidad
        var libro = new Libro();
        libro.Nombre = libroDto.Nombre;
        libro.FechaPublicacion = libroDto.FechaPublicacion;
        libro.AutorId = libroDto.AutorId;
        libro.EditorialId = libroDto.EditorialId;
 
        //Persistencia objeto
        libro = await repository.AddAsync(libro);
        //await unitOfWork.SaveChangesAsync();

        //Mapeo Entidad => Dto
        
        var libroCreada = new LibroDto();
        libroCreada.Nombre = libro.Nombre;
        libroCreada.FechaPublicacion = libro.FechaPublicacion;
        libroCreada.Id = libro.Id;
        libroCreada.AutorId = libro.AutorId;
        libroCreada.EditorialId = libro.EditorialId;
     


        //TODO: Enviar un correo electronica... 

        return libroCreada;
    }

    public async Task UpdateAsync(int id, LibroCrearActualizarDto libroDto)
    {
        var libro = await repository.GetByIdAsync(id);
        if (libro == null){
            throw new ArgumentException($"El libro con el id: {id}, no existe");
        }
        
        var existeNombreLibro = await repository.ExisteNombre(libroDto.Nombre,id);
        if (existeNombreLibro){
            throw new ArgumentException($"Ya existe un libro con el nombre {libroDto.Nombre}");
        }

        //Mapeo Dto => Entidad
        libro.Nombre = libroDto.Nombre;
        libro.FechaPublicacion = libroDto.FechaPublicacion;
        libro.AutorId = libroDto.AutorId;
        libro.EditorialId = libroDto.EditorialId;

        //Persistencia objeto
        await repository.UpdateAsync(libro);
        //await unitOfWork.SaveChangesAsync();

        return;
    }

    public async Task<bool> DeleteAsync(int libroId)
    {
        //Reglas Validaciones... 
        var libro = await repository.GetByIdAsync(libroId);
        if (libro == null){
            throw new ArgumentException($"El libro con el id: {libroId}, no existe");
        }

        repository.Delete(libro);
        //await unitOfWork.SaveChangesAsync();

        return true;
    }

    public ICollection<LibroDto> GetAll()
    {
        var libroList = repository.GetAllIncluding(x=>x.Autor, x=>x.Editorial);

        var libroListDto =  libroList.Select(
                            x=>new LibroDto(){
                                Id = x.Id,
                                Nombre = x.Nombre,
                                FechaPublicacion = x.FechaPublicacion,
                                Autor = x.Autor.Nombre,
                                AutorId = x.AutorId,
                                Editorial = x.Editorial.Nombre,
                                EditorialId = x.EditorialId
                            }
        );

        return libroListDto.ToList();
    }

    
}