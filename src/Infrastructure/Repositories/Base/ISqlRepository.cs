using Domain.Repositories.Base;

namespace Infrastructure.Repositories.Base;

public interface ISqlRepository
{
	IUnitOfWork UnitOfWork { get; }
}
