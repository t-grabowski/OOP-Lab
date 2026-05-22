InvalidInputErr = "Invalid input"
CannotDivideByZeroErr = "Cannot divide by zero"

def calculator():
	print("Input (a, b, operator): ", end="")
	user_input = read_values()
	if (len(user_input) != 3):
		print(InvalidInputErr)
		return

	if (any(not num.isdecimal() for num in user_input[0:2])):
		print(InvalidInputErr)
		return

	nums = list(map(lambda num: int(num), user_input[0:2]))

	result = 0
	match user_input[2]:
		case "+":
			result = nums[0] + nums[1]
		case "-":
			result = nums[0] - nums[1]
		case "*":
			result = nums[0] * nums[1]
		case "/" if nums[1] == 0:
			print(CannotDivideByZeroErr)
			return
		case "/":
			result = nums[0] / nums[1]
		case _:
			print(InvalidInputErr)
			return

	print("Result: ", result)


def temp_converter():
	print("Input (C/F, temp): ", end="")
	user_input = read_values()
	if (len(user_input) != 2 or not user_input[1].isdecimal()):
		print(InvalidInputErr)
		return

	temp = float(user_input[1])

	result = 0
	match user_input[0].lower():
		case "c":
			result = celsius_to_fehrenheit(temp)
		case "f":
			result = fehrenheit_to_celsius(temp)
		case _:
			print("Invalid operation")
			return

	print("Result: ", result)


def grade_average():
	passing_threshold = 3.0

	print("Input grades separated by commas: ", end="")
	user_input = read_values()
	if (user_input == 0):
		print(InvalidInputErr)
		return

	grades = []
	for gradeStr in user_input:
		if (not gradeStr.isdecimal()):
			print(InvalidInputErr)
			return

		grade = int(gradeStr)
		if (grade < 1 or grade > 6):
			print(InvalidInputErr)
			return

		grades.append(grade)

	avg = sum(grades) / len(grades)
	didPass = avg >= passing_threshold
	print("Average: ", avg)
	print("Did pass: ", didPass)


def fehrenheit_to_celsius(f: float) -> float:
	return (f - 32) / 1.8


def celsius_to_fehrenheit(c: float) -> float:
	return c * 1.8 + 32


def read_values():
	return list(map(lambda x: x.strip(), input().split(",")))

print("1. Calculator");
print("2. Temperature Converter");
print("3. Grade average");

user_input = input()
if (not user_input.isdecimal()):
	print(InvalidInputErr)
	exit(1)

match int(user_input):
	case 1:
		calculator()
	case 2:
		temp_converter()
	case 3:
		grade_average()
	case _:
		print(InvalidInputErr)
		exit(1)

