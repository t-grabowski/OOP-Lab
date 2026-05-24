from show import Show, ShowType
from showing import Showing
from room import Room, Seat
from customer import Customer

room = Room(1, 100, 10);
show = Show("Test1", ShowType.MOVIE, 0);
showing = Showing(show, 0, room);
customer = Customer("test@example.com");

for i in range(2):
	print(i, end=": ")
	seat = Seat(5)
	result = showing.reserve_seat(customer, seat)
	if(isinstance(result, Exception)):
		print(result)
		continue

	print("ticket seat", result.seat.number, "for show", result.showing.show.title)


