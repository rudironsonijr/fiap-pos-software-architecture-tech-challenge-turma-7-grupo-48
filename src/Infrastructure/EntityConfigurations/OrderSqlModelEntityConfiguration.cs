using Infrastructure.SqlModels.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

internal class OrderSqlModelEntityConfiguration : IEntityTypeConfiguration<OrderSqlModel>
{
	public void Configure(EntityTypeBuilder<OrderSqlModel> builder)
	{
		builder.HasKey(x => x.Id);

		builder.Property(c => c.Status)
				.HasConversion<string>();

		builder.Property(c => c.PaymentProvider)
			.HasConversion<string>();

		builder.Property(c => c.PaymentKind)
			.HasConversion<string>();

		builder
			.Property(x => x.Price)
			.HasColumnType("decimal(18,4)");

		builder.HasOne(x => x.Customer)
			.WithMany(x => x.Orders)
			.HasForeignKey(x => x.CustomerId)
			.IsRequired(false);

	}
}
