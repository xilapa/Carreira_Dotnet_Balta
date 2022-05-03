using System.Threading;
using System.Threading.Tasks;
using Domain.Repositories;

namespace Tests.Repositories;

public class FakeUnitOfWork : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(0);
    }
}