from show import Show
from room import Room, Seat
from ticket import Ticket

ErrSeatNotFound = Exception("Seat was not found.")
ErrSeatTaken = Exception("Seat is taken.")

class Showing:
	def __init__(self, show, start_time, room):
		self.show = show
		self.start_time = start_time
		self.room = room
		self.reservations = dict()

	def reserve_seat(self, customer, seat):
		is_reserved = self.is_seat_reserved(seat)
		if(isinstance(is_reserved, Exception)):
			return is_reserved

		if(is_reserved):
			return ErrSeatTaken

		ticket = Ticket(customer, self, seat)
		self.reservations[seat] = ticket
		return ticket


	def is_seat_reserved(self, seat) -> bool | Exception:
		if(seat.number > self.room.seat_amount):
			return ErrSeatNotFound

		return seat in self.reservations
