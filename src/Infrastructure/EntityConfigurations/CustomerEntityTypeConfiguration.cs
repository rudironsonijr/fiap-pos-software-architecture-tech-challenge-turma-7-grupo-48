using Domain.ValueObjects;
using Infrastructure.SqlModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
namespace Infrastructure.EntityConfigurations;

internal class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<CustomerSqlModel>
{
	public void Configure(EntityTypeBuilder<CustomerSqlModel> builder)
	{
		builder.HasKey(x => x.Id);

		builder.Property(x => x.Name)
			.IsRequired()
			.HasColumnType("varchar(200)");

		builder.Property(c => c.Cpf)
				.IsRequired()
				.HasColumnType("varchar(11)");


		builder.Property(x => x.Email)
			.IsRequired()
			.HasColumnType("varchar(320)");
	}
}
