using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IArtistRepository : IGenericRepository<Artist>
{
    Task<bool> EmailExistsAsync(string email);
}
