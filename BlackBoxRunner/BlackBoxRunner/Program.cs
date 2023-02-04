namespace BlackBoxRunner;

internal static class Program
{
	public static void Main()
	{
		string? inputString;
		OutputText();
		while (!"N".Equals(inputString = Console.ReadLine(), StringComparison.InvariantCultureIgnoreCase))
		{
			var result = BlackBoxComponent.BlackBox.Magic(inputString ?? string.Empty);
			Console.WriteLine($"Magic Result is: {result}");
			OutputText();
		}
		
		Console.WriteLine("Goodbye!");
	}

	private static void OutputText()
	{
		Console.WriteLine("Input text to put through the BlackBox. Input 'N' to stop");
	}
}