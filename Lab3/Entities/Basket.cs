using Dumpify;
using Lab3.SeedWork;

namespace Lab3.Entities;

public sealed record ProductInBasket {
	private Guid ProductId;
	public Product Product { get; init; }
	public uint Quantity { get; init; }

	public ProductInBasket(Product product, uint quantity) {
		ProductId =  product.Id;
		Product = product;
		Quantity = quantity;
	}
	public ProductInBasket IncreaseQuantity(uint quantity) =>
		this with { Quantity = Quantity + quantity };
	public ProductInBasket DecreaseQuantity(uint quantity) =>
		this with { Quantity = Quantity - quantity };

	// for EF
	private ProductInBasket(){}
};

public sealed class Basket : Entity<Guid> {
	public List<ProductInBasket> Products { get; private init; } = [];

	public float OriginalTotalPrice => Products
		.Sum(p => p.Product.OriginalPrice * p.Quantity);

	public float DiscountedTotalPrice => ApplyDiscounts();

	public Basket()
		: base(Guid.CreateVersion7()){ }

	public void AddProduct(Product product, uint quantity = 1) {
		var inBasket = Products.SingleOrDefault(x => x.Product == product);
		if (inBasket is null) {
			Products.Add(new ProductInBasket(product, quantity));
			return;
		}

		Products.Remove(inBasket);
		Products.Add(inBasket.IncreaseQuantity(quantity));
	}

	public void RemoveProduct(Product product, uint quantity = 1) {
		var inBasket = Products.SingleOrDefault(x => x.Product == product);
		if (inBasket is null) {
			return;
		}

		Products.Remove(inBasket);

		if (inBasket.Quantity > quantity) {
			Products.Add(inBasket.DecreaseQuantity(quantity));
		}

	}

	public bool HasProduct(Product product) =>
		Products.Any(p => p.Product == product);

	public List<Discount> GetDiscounts()
		=> Products
			.SelectMany(p => p.Product.Discounts)
			.ToList();

	public float ApplyDiscounts() {
		var deduction = Products
			.Select(p => p.Product)
			.Sum(p => p.Discounts
				.Sum(d => d.ApplyDiscount(this, p))
			);

		return OriginalTotalPrice - deduction.Dump("deduction");
	}

}

