namespace Lab2;

public sealed record Ticket(
	Customer Customer,
	Showing Showing,
	Seat AssignedSeat
);

