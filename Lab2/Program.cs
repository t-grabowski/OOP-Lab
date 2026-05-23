using Lab2;

var room = new Room(1, 100, 10);
var show = new Show("Test1", ShowType.Movie, TimeSpan.FromHours(2));
var showing = new Showing(show, DateTime.Today.AddDays(1).AddHours(17), room);
var customer = new Customer("test@example.com");

string? message = null;
var messageIsError = false;
while (true) {
	Console.Clear();
	if (message is not null) {
		if (messageIsError) Console.ForegroundColor = ConsoleColor.Red;
		Console.WriteLine(message);
		message = null;
		messageIsError = false;
		Console.ResetColor();
	}
	Console.WriteLine($"room: {room.Id}, show: {show.Title}, start time: {showing.StartTime}, end time: {showing.EndTime}");
	showing.Print();
	Console.Write("Choose a seat: ");
	if (!int.TryParse(Console.ReadLine(), out var input)) {
		message = "Invalid input";
		continue;
	}

	var seat = new Seat(input);
	var reserveResult = showing.ReserveSeat(customer, seat);
	if (reserveResult.IsError) {
		message = reserveResult.FirstError.Description;
		messageIsError = true;
		continue;
	}

	message = $"Seat '{reserveResult.Value.AssignedSeat.ToString()}' was reserved successfully\n";
}

