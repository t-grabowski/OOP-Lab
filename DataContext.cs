using Lab3.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lab3;

public sealed class DataContext : DbContext {
	public DbSet<Product> Products { get; set; }
	public DbSet<Basket> Baskets { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
		optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
	}
}

