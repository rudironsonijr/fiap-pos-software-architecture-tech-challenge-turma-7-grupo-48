namespace Domain.Repositories.Base;

public interface IUnitOfWork
{
	Task<int> CommitAsync(CancellationToken cancellationToken = default);

}
