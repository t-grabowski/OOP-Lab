using Lab3.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lab3.Data;

public sealed class ProductsConfiguration :  IEntityTypeConfiguration<Product> {
	public void Configure(EntityTypeBuilder<Product> builder) {
		builder.HasKey(p => p.Id);
	}
}

public sealed class DiscountsConfiguration : IEntityTypeConfiguration<Discount> {
	public void Configure(EntityTypeBuilder<Discount> builder) {
		builder.HasKey(d => d.Id);
		builder.HasDiscriminator<string>("DiscountType")
			.HasValue<AmountDiscount>(nameof(AmountDiscount))
			.HasValue<PercentDiscount>(nameof(PercentDiscount))
			.HasValue<TwoForOneDiscount>(nameof(TwoForOneDiscount));
	}
}

public sealed class BasketConfiguration : IEntityTypeConfiguration<Basket> {
	public void Configure(EntityTypeBuilder<Basket> builder) {
		builder.HasKey(b => b.Id);

		builder.OwnsMany(b => b.Products, pb => {
				pb.ToTable("BasketProducts");
				pb.Property<Guid>("Id").ValueGeneratedOnAdd();
				pb.HasKey("Id");

				pb.HasOne(p => p.Product)
					.WithMany()
					.HasForeignKey("ProductId");
		});
	}
}


