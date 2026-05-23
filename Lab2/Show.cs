namespace Lab2;

public enum ShowType {
	Movie,
	Play,
}

public sealed class Show {
	public string Title { get; set; }
	public ShowType Type { get; set; }

	public Show(string title, ShowType type) {
		Title = title;
		Type = type;
	}
}


