using CodingDojo.CardWars.Data;

namespace CodingDojo.CardWars.Infrastructure;

public interface ICardWarProcessor
{
	GameResultType ProcessInput(CardWarInputData inputData);
}