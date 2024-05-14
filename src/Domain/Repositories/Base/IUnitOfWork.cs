namespace Domain.Repositories.Base;

public interface IUnitOfWork
{
	Task<bool> CommitAsync(CancellationToken cancellationToken = default);

}
