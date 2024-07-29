using Infrastructure.SqlModels.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

internal class ProductSqlModelEntityConfiguration : IEntityTypeConfiguration<ProductSqlModel>
{
	public void Configure(EntityTypeBuilder<ProductSqlModel> builder)
	{
		builder.HasKey(x => x.Id);

		builder.Property(c => c.Name)
			.HasColumnType("varchar(100)")
			.IsRequired();

		builder.Property(c => c.Description)
			.HasColumnType("varchar(MAX)")
			.IsRequired();

		builder.Property(c => c.ProductType)
			.HasConversion<string>();

		builder.Property(c => c.PhotoFilename)
			.HasColumnType("varchar(200)");

		builder.Property(c => c.PhotoContentType)
			.HasColumnType("varchar(50)");

		builder
			.Property(x => x.Price)
			.HasColumnType("decimal(18,4)");
	}
}
