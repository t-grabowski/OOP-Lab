using ErrorOr;

namespace Lab2;

public sealed class Showing {
	public Show Show { get; set; }
	public DateTime StartTime { get; set; }
	public TimeSpan Duration { get; set; }
	public Room Room { get; set; }
	public Dictionary<Seat, Ticket> Reservations { get; set; }

	public DateTime EndTime => StartTime + Duration;

	public ErrorOr<Ticket> ReserveSeat(Customer customer, Seat seat) {
		var isReserved = IsSeatReserved(seat);
		if (isReserved.IsError) {
			return isReserved.Errors;
		}

		if (isReserved.Value) {
			return Errors.SeatTaken;
		}

		var ticket = new Ticket(customer, this, seat);
		Reservations[seat] = ticket;
		return ticket;
	}

	public ErrorOr<bool> IsSeatReserved(Seat seat) {
		if (seat.Number > Room.SeatAmount) {
			return Errors.SeatNotFound;
		}

		return Reservations.TryGetValue(seat, out _);
	}

	public Showing(Show show, DateTime startTime, TimeSpan duration, Room room) {
		Reservations = [];
		Show = show;
		StartTime = startTime;
		Duration = duration;
		Room = room;
	}

	public static class Errors {
		public static Error SeatNotFound => Error.NotFound("seat_not_found", "Seat not found.");
		public static Error SeatTaken => Error.Conflict("seat_taken", "Seat is already taken");
	}
}

public static class ShowingTui {
	extension(Showing showing) {
		public void Print(
			ConsoleColor fontColor = ConsoleColor.Black,
			ConsoleColor reservedColor = ConsoleColor.Red,
			ConsoleColor freeColor = ConsoleColor.Green) {

			var rows = (int)Math.Ceiling((double)showing.Room.SeatAmount / showing.Room.SeatColumns);
			var cellLength = Math.Max(showing.Room.SeatAmount.ToString().Length, 1);

			const char screenPadChar = '-';
			var frontText = showing.Show.Type switch {
				ShowType.Movie => "SCREEN",
				ShowType.Play => "STAGE",
			};
			var frontLength = cellLength * showing.Room.SeatColumns + showing.Room.SeatColumns - 1;
			var front = frontText
				.PadLeft((frontLength - frontText.Length)/2 + frontText.Length, screenPadChar)
				.PadRight(frontLength, screenPadChar);

			Console.WriteLine(front);
			for (var i = 1; i <= showing.Room.SeatAmount; i += showing.Room.SeatColumns) {
				for (var j = 0; j < showing.Room.SeatColumns; j++) {
					Console.ForegroundColor = fontColor;
					var currSeat = new Seat(i + j);
					var isReserved = showing.IsSeatReserved(currSeat);
					if (isReserved.IsError) {
						Console.Write("  ");
						continue;
					}

					Console.BackgroundColor = isReserved.Value ?
						reservedColor :
						freeColor;

					Console.Write(currSeat.ToString().PadLeft(cellLength, ' '));
					Console.ResetColor();
					Console.Write(" ");
				}
				Console.WriteLine();
			}
		}
	}
}

