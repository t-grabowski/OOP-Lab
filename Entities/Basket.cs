using Lab3.SeedWork;

namespace Lab3.Entities;

public sealed class Basket : Entity<Guid> {
	public Dictionary<Product, uint> Products { get; private init; } = [];

	public float OriginalTotalPrice => Products
		.Sum(p => p.Key.OriginalPrice * p.Value);

	public float DiscountedTotalPrice => ApplyDiscounts();

	public Basket()
		: base(Guid.CreateVersion7()){ }

	public void AddProduct(Product product, uint quantity = 1) {
		var qty = Products.GetValueOrDefault(product, (uint)0);
		Products[product] = qty + quantity;
	}

	public void RemoveProduct(Product product) {
		if (!Products.TryGetValue(product, out var qty)) {
			return;
		}

		if (qty == 1) {
			Products.Remove(product);
			return;
		}

		Products[product] = qty - 1;
	}

	public List<IDiscount> GetDiscounts()
		=> Products
			.SelectMany(p => p.Key.Discounts)
			.ToList();

	public float ApplyDiscounts() {
		var deduction = Products.Keys
			.Sum(p => p.Discounts
				.Sum(d => d.ApplyDiscount(this, p))
			);

		return OriginalTotalPrice - deduction;
	}

}

