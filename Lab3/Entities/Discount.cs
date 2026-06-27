using Dumpify;
using Lab3.SeedWork;

namespace Lab3.Entities;

public abstract class Discount : Entity<Guid> {
	public string? Description { get; init; } = null;

	protected Discount() : base(Guid.CreateVersion7()) { }

	protected Discount(string description) : this() {
		Description = description;
	}

	public abstract float ApplyDiscount(Basket basket, Product product);
}

public sealed class AmountDiscount(float amount) : Discount {
	public float Value { get; set; } = amount;

	public override float ApplyDiscount(Basket _1, Product _2) => Value;

	// for EF
	private AmountDiscount() : this(0) {}
}

/// <param name="percent"> use whole numbers  15 -> 15% off, not 0.15</param>
public sealed class PercentDiscount(int percent) : Discount {
	public int Value { get; set; } = percent;

	public override float ApplyDiscount(Basket basket, Product product) =>
		(product.OriginalPrice * (Value / 100f));

	// for EF
	private PercentDiscount() : this(0) {}
}

public sealed class TwoForOneDiscount() : Discount{
	public override float ApplyDiscount(Basket basket, Product product) {
		var inBasket = basket.Products.SingleOrDefault(b => b.Product == product);
		if (inBasket is null) {
			throw new Exception("Product not found in basket. should never throw");
		}

		return float.Floor((float)inBasket.Quantity / 2) * product.OriginalPrice;
	}
}
