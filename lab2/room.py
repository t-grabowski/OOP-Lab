class Seat:
	def __init__(self, number):
		self.number = number

	def __hash__(self):
		return self.number.__hash__()

	def __eq__(self, o):
		if(not isinstance(o, Seat)):
			return False

		return self.__hash__() is o.__hash__()

class Room:
	def __init__(self, room_id, seat_amount, seat_columns):
		self.room_id = room_id
		self.seat_amount = seat_amount
		self.seat_columns = seat_columns

