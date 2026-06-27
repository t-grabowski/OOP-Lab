using Dumpify;
using Lab3.Data;
using Lab3.Entities;
using Microsoft.EntityFrameworkCore;

var db = DataContext.Create();

db.Database.EnsureDeleted();
db.Database.EnsureCreated();

await db.Seed();

var basket = await db.Baskets
	.Include(b => b.Products)
		.ThenInclude(p => p.Product)
		.ThenInclude(p => p.Discounts)
	.FirstOrDefaultAsync();

if (basket is null) {
	throw new Exception("basket is null");
}

basket.Print();


return;

public static class Extensions {
	public static async Task Seed(this DataContext db) {
		List<Product> products = [
			new("Test1", 50, [new PercentDiscount(10)]),
			new("Test2", 10, [new TwoForOneDiscount()]),
		];

		var basket = new Basket();
		basket.AddProduct(products[0]);
		basket.AddProduct(products[1], 2);

		await db.Baskets.AddAsync(basket);
		await db.SaveChangesAsync();
	}

	public static void Print(this Basket basket) {
		Console.WriteLine("===========");
		Console.WriteLine("Produkty:");
		foreach (var product in basket.Products) {
			Console.WriteLine($"- {product.Product.Name} | {product.Product.OriginalPrice} x {product.Quantity}");
		}
		Console.WriteLine("-----------");
		Console.WriteLine($"Cena przed zastosowaniem zniżek: {basket.OriginalTotalPrice}");
		Console.WriteLine($"Cena po zastosowaniu zniżek: {basket.DiscountedTotalPrice}");
	}
}
