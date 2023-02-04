using System.ComponentModel;
using CodingDojo.CardWars.Data;
using CodingDojo.CardWars.Extensions;
using CodingDojo.CardWars.Infrastructure;

namespace CodingDojo.CardWars.Implementation;

public class CardWarProcessor : ICardWarProcessor
{
	private bool _additionOrder;

	public CardWarProcessor(bool additionOrder)
	{
		_additionOrder = additionOrder;
	}

	public GameResultType ProcessInput(CardWarInputData inputData)
	{
		if (inputData.PlayerOneStartingDeck == null || inputData.PlayerTwoStartingDeck == null)
		{
			throw new ArgumentNullException(nameof(inputData));
		}

		var deck1 = new Queue<CardValue>(inputData.PlayerOneStartingDeck);
		var deck2 = new Queue<CardValue>(inputData.PlayerTwoStartingDeck);

		if (deck1.Any(c => !Enum.IsDefined(typeof(CardValue), c))
		    || deck2.Any(c => !Enum.IsDefined(typeof(CardValue), c)))
		{
			throw new InvalidEnumArgumentException();
		}

		if (deck1.Count != deck2.Count)
		{
			throw new InvalidDataException();
		}

		var player1PlayedCards = new List<CardValue>();
		var player2PlayedCards = new List<CardValue>();
		return GetGameResult(deck1, deck2, player1PlayedCards, player2PlayedCards, _additionOrder);
	}

	public void SetOrderType(bool orderType)
	{
		_additionOrder = orderType;
	}

	private static GameResultType GetGameResult(Queue<CardValue> deck1, Queue<CardValue> deck2,
		List<CardValue> player1PlayedCards, List<CardValue> player2PlayedCards, bool isLoserFirst)
	{
		var isDeck1Empty = !deck1.Any();
		var isDeck2Empty = !deck2.Any();

		while (!isDeck1Empty && !isDeck2Empty)
		{
			PrintDecks(deck1, deck2);
			isDeck1Empty = !deck1.TryDequeue(out var deck1Fighter);
			isDeck2Empty = !deck2.TryDequeue(out var deck2Fighter);

			if (CompareDeckEmptiness(isDeck1Empty, isDeck2Empty, out var result))
			{
				Console.WriteLine(result);
				return result;
			}

			player1PlayedCards.Add(deck1Fighter);
			player2PlayedCards.Add(deck2Fighter);

			var compare = deck1Fighter.CompareTo(deck2Fighter);

			switch (compare)
			{
				case 0:
				{
					var isDeck1EmptyAfterDraw = !deck1.TryGetCardsFromDeck(out var cards1);
					var isDeck2EmptyAfterDraw = !deck2.TryGetCardsFromDeck(out var cards2);
					if (CompareDeckEmptiness(isDeck1EmptyAfterDraw, isDeck2EmptyAfterDraw, out var afterTieResult))
					{
						Console.WriteLine(afterTieResult);
						return afterTieResult;
					}

					player1PlayedCards.AddRange(cards1);
					player2PlayedCards.AddRange(cards2);
					break;
				}
				case > 0:
					deck1.AddCardsToTheBack(player1PlayedCards, player2PlayedCards, isLoserFirst);
					break;
				case < 0:
					deck2.AddCardsToTheBack(player2PlayedCards, player1PlayedCards, isLoserFirst);
					break;
			}
		}

		CompareDeckEmptiness(isDeck1Empty, isDeck2Empty, out var finalResult);
		return finalResult;
	}

	private static void PrintDecks(IEnumerable<CardValue> deck1, IEnumerable<CardValue> deck2)
	{
		var s1 = deck1.Aggregate(string.Empty, (current, d) => current + d + " ");
		var s2 = deck2.Aggregate(string.Empty, (current, d) => current + d + " ");
		Console.WriteLine(s1 + " VS " + s2);
	}

	private static bool CompareDeckEmptiness(bool isDeck1Empty, bool isDeck2Empty, out GameResultType result)
	{
		if (isDeck1Empty && isDeck2Empty)
		{
			result = GameResultType.Tie;
			return true;
		}

		if (isDeck1Empty)
		{
			result = GameResultType.PlayerTwo;
			return true;
		}

		if (isDeck2Empty)
		{
			result = GameResultType.PlayerOne;

			return true;
		}

		result = GameResultType.Tie;
		return false;
	}
}