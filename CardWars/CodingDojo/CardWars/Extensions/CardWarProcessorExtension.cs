using CodingDojo.CardWars.Data;

namespace CodingDojo.CardWars.Extensions;

public static class CardWarProcessorExtension
{
	public static void AddCardsToTheBack(
		this Queue<CardValue> stack,
		List<CardValue> winnerCards,
		List<CardValue> loserCards,
		bool isLoserFirst = true
	)
	{
		for (var i = 0; i < winnerCards.Count || i < loserCards.Count; i++)
		{
			if (isLoserFirst)
			{
				if (i < loserCards.Count)
				{
					stack.Enqueue(loserCards[i]);
				}

				if (i < winnerCards.Count)
				{
					stack.Enqueue(winnerCards[i]);
				}
			}
			else
			{
				if (i < winnerCards.Count)
				{
					stack.Enqueue(winnerCards[i]);
				}

				if (i < loserCards.Count)
				{
					stack.Enqueue(loserCards[i]);
				}
			}
		}

		winnerCards.Clear();
		loserCards.Clear();
	}

	public static bool TryGetCardsFromDeck(
		this Queue<CardValue> stack,
		out List<CardValue> cards,
		int amountToGet = 3
	)
	{
		cards = new List<CardValue>();
		while (amountToGet-- > 0)
		{
			if (stack.TryDequeue(out var result))
			{
				cards.Add(result);
			}
			else
			{
				return false;
			}
		}

		return true;
	}
}