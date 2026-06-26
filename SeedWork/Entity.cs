namespace Lab3.SeedWork;

public abstract class Entity<TId>(TId id) where TId : notnull {
	public TId Id { get; private init; } = id;

	public override int GetHashCode() => Id.GetHashCode();

	public override bool Equals(object? obj) =>
		obj is Entity<TId> other && Id.Equals(other.Id);

	public static bool operator ==(Entity<TId> left, Entity<TId> right) => left.Equals(right);
	public static bool operator !=(Entity<TId> left, Entity<TId> right) => !left.Equals(right);
}

