using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IDSPRepository : IGenericRepository<DSP>
{
    Task<List<DSP>> GetByIdsAsync(List<int> ids);
}
