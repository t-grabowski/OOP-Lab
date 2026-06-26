using Lab3.SeedWork;

namespace Lab3.Entities;

public sealed class Product : Entity<Guid> {
	public string Name { get; private set; }
	public float OriginalPrice { get; private set; }
	public List<Discount> Discounts { get; private set; }

	public Product(string name, float originalPrice, IEnumerable<Discount> discounts)
		: base(Guid.CreateVersion7()){
		Name = name;
		OriginalPrice = originalPrice;
		Discounts = discounts.ToList();
	}

	/// <summary>
	/// For EF
	/// </summary>
	private Product() : base(Guid.Empty) { }

}

