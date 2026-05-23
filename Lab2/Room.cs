namespace Lab2;

public readonly record struct Seat(int Number) {
	public override string ToString() => Number.ToString();
};

public sealed class Room {
	public int Id { get; set; }
	public int SeatAmount { get; set; }
	public int SeatColumns { get; set; }

	public Room(int id, int seatAmount, int seatColumns) {
		Id = id;
		SeatAmount = seatAmount;
		SeatColumns = seatColumns;
	}
}


