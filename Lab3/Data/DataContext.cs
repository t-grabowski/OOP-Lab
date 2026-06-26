using Lab3.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lab3.Data;

public sealed class DataContext(DbContextOptions<DataContext> options)
	: DbContext(options)
{
	public DbSet<Product> Products { get; set; }
	public DbSet<Discount> Discounts { get; set; }
	public DbSet<Basket> Baskets { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
		optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
	}

	public static DataContext Create() {
		var connectionString = GetConnectionString();
		var dbOpts = new DbContextOptionsBuilder<DataContext>()
			.UseSqlite(connectionString);

		return new DataContext(dbOpts.Options);
	}

	private static string GetConnectionString() {
		var projDir = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName;
		if (projDir is null) {
			throw new Exception("cannot get current project directory");
		}
		var dbPath = Path.Join(projDir, "lab3.db");
		var connStr =  $"Data Source={dbPath};";
		Console.WriteLine(connStr);
		return connStr;
	}
}
