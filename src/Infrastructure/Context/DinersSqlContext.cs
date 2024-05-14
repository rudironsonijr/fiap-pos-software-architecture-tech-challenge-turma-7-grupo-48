using Domain.Repositories.Base;
using Infrastructure.EntityConfigurations;
using Infrastructure.SqlModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Context;

public class DinersSqlContext : DbContext, IUnitOfWork
{
	public DinersSqlContext(DbContextOptions<DinersSqlContext> options) : base(options) { }
	internal DbSet<CustomerSqlModel> Customer { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new CustomerSqlModelEntityTypeConfiguration());

		base.OnModelCreating(modelBuilder);
	}

	public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken token = default)
	{
		var entries = ChangeTracker
			.Entries()
			.Where(e => e.Entity is BaseSqlModel &&
				(e.State == EntityState.Added || e.State == EntityState.Modified));

		foreach (var entityEntry in entries)
		{
			SetOperationDate(entityEntry);
		}

		return
			base.SaveChangesAsync(acceptAllChangesOnSuccess, token);
	}

	private void SetOperationDate(EntityEntry entityEntry)
	{
		((BaseSqlModel)entityEntry.Entity).UpdatedAt = DateTime.Now;

		if (entityEntry.State == EntityState.Added)
		{
			((BaseSqlModel)entityEntry.Entity).CreatedAt = DateTime.Now;
		}
	}

	public Task<int> CommitAsync(CancellationToken cancellationToken = default)
	{
		return base.SaveChangesAsync(cancellationToken);

	}
}