using Curso.ComercioElectronico.Domain;
using Microsoft.EntityFrameworkCore;

namespace Curso.ComercioElectronico.Infraestructure;

public class OrdenRepository : EfRepository<Orden,Guid>, IOrdenRepository
{
    public OrdenRepository(ComercioElectronicoDbContext context) : base(context)
    {
    }
    
}