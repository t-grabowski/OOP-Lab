Console.WriteLine("1. Calculator");
Console.WriteLine("2. Temperature Converter");
Console.WriteLine("3. Grade average");

if (!int.TryParse(Console.ReadLine()?.Trim(), out var menuInput)) {
	Console.WriteLine(Errors.InvalidInput);
	return;
}

switch (menuInput) {
	case 1:
		Calculator();
		break;
	case 2:
		TempConverter();
		break;
	case 3:
		GradeAverage();
		break;
	default:
		Console.WriteLine(Errors.InvalidInput);
		break;
}
return;

static void Calculator() {
	Console.Write("Input (a, b, operator): ");
	var input = ReadValues();

	const int numOfNums = 2;
	if (input is null || input.Length != 3 || input.Take(numOfNums).Any(n => !int.TryParse(n, out _))) {
		Console.WriteLine(Errors.InvalidInput);
		return;
	}

	var nums = input
		.Take(numOfNums)
		.Select(int.Parse)
		.ToArray();

	(float value, string? error) result = input[2] switch {
		"+" => (nums.Sum(), null),
		"-" => (nums[0] - nums[1], null),
		"*" => (nums[0] * nums[1], null),
		"/" when nums[1] == 0 => (0, Errors.CannotDivideByZero),
		"/" => ((float)nums[0] / nums[1], null),
		_ => (0, Errors.InvalidInput),
	};

	if (result.error is not null) {
		Console.WriteLine(result.error);
		return;
	}

	Console.WriteLine($"Result: {result.value}");
}

static void TempConverter() {
	Console.Write("Input (C/F, temp): ");
	var input = ReadValues();
	if (input is null || input.Length != 2 || !float.TryParse(input[1], out var temp)) {
		Console.WriteLine(Errors.InvalidInput);
		return;
	}

	(float value, string? error) result = input[0].ToLower() switch {
		"c" => (CelsiusToFahrenheit(temp), null),
		"f" => (FahrenheitToCelsius(temp), null),
		_ => (0, Errors.InvalidInput),
	};

	if (result.error is not null) {
		Console.WriteLine(Errors.InvalidInput);
		return;
	}

	Console.WriteLine($"Result: {result.value}");
}

static void GradeAverage(float passingThreshold = 3.0f) {
	Console.Write("Input grades separated by commas: ");
	var input = ReadValues();
	if (input is null) {
		Console.WriteLine(Errors.InvalidInput);
		return;
	}

	var grades = new List<int>(input.Length);
	foreach (var gradeStr in input) {
		if(!int.TryParse(gradeStr, out var grade) || grade is < 1 or > 6) {
			Console.WriteLine($"{Errors.InvalidInput}: {gradeStr}");
			return;
		}
		grades.Add(grade);
	}

	var avg = (float)grades.Sum() / grades.Count;
	var didPass = avg >= passingThreshold;

	Console.WriteLine($"Grade Average: {avg}");
	Console.WriteLine($"Passed: {didPass}");
}

static float FahrenheitToCelsius(float f) => (f - 32) / 1.8f;
static float CelsiusToFahrenheit(float c) => c * 1.8f + 32;

static string[]? ReadValues() => Console.ReadLine()?
	.Split(',')
	.Select(x => x.Trim())
	.ToArray();

internal static class Errors {
	public const string? InvalidInput = "Invalid input";
	public const string? CannotDivideByZero = "Cannot divide by zero";
}
