using Infrastructure.SqlModels.PaymentAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

internal class PaymentSqlModelEntityType
{
	public void Configure(EntityTypeBuilder<PaymentSqlModel> builder)
	{
		builder.HasKey(x => x.Id);
		builder.Property(c => c.Status)
		.HasConversion<string>();

		builder.Property(c => c.PaymentProvider)
			.HasConversion<string>();

		builder.Property(c => c.PaymentKind)
			.HasConversion<string>();
	}
}
