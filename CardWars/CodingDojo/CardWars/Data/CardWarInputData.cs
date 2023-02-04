namespace CodingDojo.CardWars.Data;

public struct CardWarInputData
{
	public IEnumerable<CardValue>? PlayerOneStartingDeck { get; }
	public IEnumerable<CardValue>? PlayerTwoStartingDeck { get; }

	public CardWarInputData(IEnumerable<CardValue> playerOneStartingDeck, IEnumerable<CardValue> playerTwoStartingDeck)
	{
		PlayerOneStartingDeck = playerOneStartingDeck;
		PlayerTwoStartingDeck = playerTwoStartingDeck;
	}
}