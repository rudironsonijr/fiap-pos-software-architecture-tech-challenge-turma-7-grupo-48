using Infrastructure.SqlModels.CustomerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

internal class CustomerSqlModelEntityTypeConfiguration : IEntityTypeConfiguration<CustomerSqlModel>
{
	public void Configure(EntityTypeBuilder<CustomerSqlModel> builder)
	{
		builder.HasKey(x => x.Id);

		builder.Property(c => c.Name)
			.HasColumnType("varchar(100)")
			.HasMaxLength(100)
			.IsRequired();

		builder.Property(c => c.Email)
			.HasColumnType("varchar(100)")
			.HasMaxLength(100)
			.IsRequired();

		builder.Property(c => c.Cpf)
			.HasColumnType("varchar(14)")
			.HasMaxLength(100)
			.IsRequired();
	}
}
