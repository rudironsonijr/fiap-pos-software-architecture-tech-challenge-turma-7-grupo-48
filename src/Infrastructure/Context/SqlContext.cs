using Infrastructure.SqlModels;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public class SqlContext : DbContext
{
	internal DbSet<CustomerSqlModel> Customer { get; set; }

	public override Task<int> SaveChangesAsync(
		bool acceptAllChangesOnSuccess,
		CancellationToken token = default
	)
	{
		var entries = ChangeTracker
			.Entries()
			.Where(
				predicate: e => e.Entity is BaseSqlModel &&
				                (
					                e.State == EntityState.Added || e.State == EntityState.Modified)
			);

		foreach (var entityEntry in entries)
		{
			((BaseSqlModel)entityEntry.Entity).UpdatedAt = DateTime.Now;

			if (entityEntry.State == EntityState.Added)
				((BaseSqlModel)entityEntry.Entity).CreatedAt = DateTime.Now;
		}

		return base.SaveChangesAsync(
			acceptAllChangesOnSuccess: acceptAllChangesOnSuccess,
			cancellationToken: token
		);
	}
}