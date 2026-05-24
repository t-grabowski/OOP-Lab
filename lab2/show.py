from enum import Enum

class ShowType(Enum):
	MOVIE = 0
	PLAY = 1

class Show:
	def __init__(self, title, show_type, duration):
		self.title = title
		self.show_type = show_type
		self.duration = duration
