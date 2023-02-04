using CodingDojo.CardWars.Data;
using CodingDojo.CardWars.Infrastructure;

namespace CodingDojo.CardWars.Implementation;

public class ExampleCardWarProcessor : ICardWarProcessor
{
	public GameResultType ProcessInput(CardWarInputData inputData)
	{
		if (inputData.PlayerOneStartingDeck.Any() && inputData.PlayerTwoStartingDeck.Any())
		{
			return GameResultType.PlayerOne;
		}
		
		return GameResultType.Tie;
	}

	public void SetOrderType(bool orderType)
	{
	}
}