using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class DSPRepository : GenericRepository<DSP>, IDSPRepository
{
    public DSPRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<List<DSP>> GetByIdsAsync(List<int> ids) =>
        await DbSet.Where(d => ids.Contains(d.Id)).ToListAsync();
}
