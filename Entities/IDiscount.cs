namespace Lab3.Entities;

public interface IDiscount {
	float ApplyDiscount(Basket basket, Product product);
}

public sealed class AmountDiscount(float amount) : IDiscount {
	public float Value { get; set; } = amount;

	public float ApplyDiscount(Basket _1, Product _2) => Value;
}

public sealed class PercentDiscount(float percent) : IDiscount {
	public float Value { get; set; } = percent;

	public float ApplyDiscount(Basket basket, Product product) {
		return product.OriginalPrice * Value;
	}
}

public sealed class TwoForOneDiscount() : IDiscount{
	public float ApplyDiscount(Basket basket, Product product) {
		if (!basket.Products.TryGetValue(product, out var qty)) {
			throw new Exception("Product not found in basket. should never throw");
		}

		return float.Floor((float)qty / 2) * product.OriginalPrice;
	}
}
