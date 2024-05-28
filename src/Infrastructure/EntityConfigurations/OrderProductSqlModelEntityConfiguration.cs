using Infrastructure.SqlModels.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

internal class OrderProductSqlModelEntityConfiguration : IEntityTypeConfiguration<OrderProductSqlModel>
{
	public void Configure(EntityTypeBuilder<OrderProductSqlModel> builder)
	{
		builder.HasKey(x => x.Id);

		builder
			.Property(x => x.ProductPrice)
			.HasColumnType("decimal(18,4)");

		builder.HasOne(x => x.Product)
			.WithMany(x => x.OrderProducts)
			.HasForeignKey(x => x.ProductId);

		builder.HasOne(x => x.Order)
			.WithMany(x => x.OrderProducts)
			.HasForeignKey(x => x.OrderId);
	}
}
