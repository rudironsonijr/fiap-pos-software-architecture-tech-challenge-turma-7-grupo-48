using Domain.Entities.Base.Interfaces;

namespace Domain.Repositories.Base;

public interface IRepository<T> where T : IAggregateRoot
{
}
