using System;
using System.Collections.Generic;
using CodingDojo.CardWars.Data;
using CodingDojo.CardWars.Implementation;
using CodingDojo.CardWars.Infrastructure;
using NUnit.Framework;

namespace CodingDojo.CardWars;

public class CodeWarsTests
{
	private ICardWarProcessor _cardWarProcessor;

	private struct MirroredResult
	{
		public GameResultType ResultType1 { get; }
		public GameResultType ResultType2 { get; }

		public MirroredResult(GameResultType resultType1, GameResultType resultType2)
		{
			ResultType1 = resultType1;
			ResultType2 = resultType2;
		}
	}

	[SetUp]
	public void Setup()
	{
		_cardWarProcessor = new CardWarProcessor(true);
	}

	[Test]
	public void IsOneNullInputThrows()
	{
		var deck1 = new List<CardValue>
		{
			CardValue.Two
		};
		var input1 = new CardWarInputData(deck1, null);
		var input2 = new CardWarInputData(null, deck1);

		void Act() => _cardWarProcessor.ProcessInput(input1);
		void Act2() => _cardWarProcessor.ProcessInput(input2);

		Assert.Throws(Is.InstanceOf<Exception>(), Act);
		Assert.Throws(Is.InstanceOf<Exception>(), Act2);
	}

	[Test]
	public void IsBothNullInputThrows()
	{
		var input1 = new CardWarInputData(null, null);
		var input2 = new CardWarInputData(null, null);

		void Act() => _cardWarProcessor.ProcessInput(input1);
		void Act2() => _cardWarProcessor.ProcessInput(input2);

		Assert.Throws(Is.InstanceOf<Exception>(), Act);
		Assert.Throws(Is.InstanceOf<Exception>(), Act2);
	}

	[Test]
	public void IsNotEquallySizedHandsThrows()
	{
		var deck1 = new List<CardValue>
		{
			CardValue.Two, CardValue.Ten
		};

		var deck2 = new List<CardValue>
		{
			CardValue.Two
		};

		var input1 = new CardWarInputData(deck1, deck2);
		var input2 = new CardWarInputData(deck2, deck1);

		void Act() => _cardWarProcessor.ProcessInput(input1);
		void Act2() => _cardWarProcessor.ProcessInput(input2);

		Assert.Throws(Is.InstanceOf<Exception>(), Act);
		Assert.Throws(Is.InstanceOf<Exception>(), Act2);
	}

	[Test]
	public void IsHigherHandWins()
	{
		var deck1 = new List<CardValue>
		{
			CardValue.Two
		};

		var deck2 = new List<CardValue>
		{
			CardValue.Ten
		};

		var mirroredResult = GetResultFromInput(deck1, deck2);
		Assert.AreEqual(GameResultType.PlayerTwo, mirroredResult.ResultType1);
		Assert.AreEqual(GameResultType.PlayerOne, mirroredResult.ResultType2);
	}

	[Test]
	public void IsEqualHandsTies()
	{
		var deck1 = new List<CardValue>
		{
			CardValue.Two
		};

		var deck2 = new List<CardValue>
		{
			CardValue.Two
		};

		var mirroredResult = GetResultFromInput(deck1, deck2);
		Assert.AreEqual(GameResultType.Tie, mirroredResult.ResultType1);
		Assert.AreEqual(GameResultType.Tie, mirroredResult.ResultType2);
	}

	[Test]
	public void IsDoubleTieTies()
	{
		var deck1 = new List<CardValue>
		{
			CardValue.Two, CardValue.Ten, CardValue.Eight, CardValue.Ace, CardValue.Two
		};

		var deck2 = new List<CardValue>
		{
			CardValue.Two, CardValue.Ten, CardValue.Eight, CardValue.Ace, CardValue.Two
		};

		var mirroredResult = GetResultFromInput(deck1, deck2);
		Assert.AreEqual(GameResultType.Tie, mirroredResult.ResultType1);
		Assert.AreEqual(GameResultType.Tie, mirroredResult.ResultType2);
	}

	[Test]
	public void IsBiggerHandAfterTieWins()
	{
		var deck1 = new List<CardValue>
		{
			CardValue.Two, CardValue.Ten, CardValue.Eight, CardValue.Ace, CardValue.Ace
		};

		var deck2 = new List<CardValue>
		{
			CardValue.Two, CardValue.Ten, CardValue.Eight, CardValue.Ace, CardValue.Two
		};

		var mirroredResult = GetResultFromInput(deck1, deck2);
		Assert.AreEqual(GameResultType.PlayerOne, mirroredResult.ResultType1);
		Assert.AreEqual(GameResultType.PlayerTwo, mirroredResult.ResultType2);
	}

	[Test]
	public void IsHigherHandTakesAndWins()
	{
		//A 10 VS 2 8
		//A - 2 => PlayerOne won round
		//10 2 A VS 8
		//10 2 A 8 => PlayerOne Wins
		var deck1 = new List<CardValue>
		{
			CardValue.Ace, CardValue.Ten
		};

		var deck2 = new List<CardValue>
		{
			CardValue.Two, CardValue.Eight
		};

		var mirroredResult = GetResultFromInput(deck1, deck2);
		Assert.AreEqual(GameResultType.PlayerOne, mirroredResult.ResultType1);
		Assert.AreEqual(GameResultType.PlayerTwo, mirroredResult.ResultType2);
	}

	[Test]
	public void IsInitiallyLowerHandWins()
	{
		//2 10 J VS 10 8 5
		//10 J VS 8 5 2 10
		//J 8 10 VS 5 2 10
		//8 10 5 J VS 2 10
		//10 5 J 2 8 VS 10
		//PlayerTwo Out of cards
		var deck1 = new List<CardValue>
		{
			CardValue.Two, CardValue.Ten, CardValue.Jack
		};

		var deck2 = new List<CardValue>
		{
			CardValue.Ten, CardValue.Eight, CardValue.Five
		};

		var mirroredResult = GetResultFromInput(deck1, deck2);
		Assert.AreEqual(GameResultType.PlayerOne, mirroredResult.ResultType1);
		Assert.AreEqual(GameResultType.PlayerTwo, mirroredResult.ResultType2);
	}

	[Test]
	public void IsBackToHandOrderRespected()
	{
		var deckKJQ = new List<CardValue>
		{
			CardValue.King, CardValue.Jack, CardValue.Queen
		};
		var deckJQK = new List<CardValue>
		{
			CardValue.Jack, CardValue.Queen, CardValue.King
		};

		var mirroredResult = GetResultFromInput(deckKJQ, deckJQK);
		Assert.AreEqual(GameResultType.PlayerTwo,mirroredResult.ResultType1);
		Assert.AreEqual(GameResultType.PlayerOne, mirroredResult.ResultType2);
	}

	[Test]
	public void IsWeirdResultThrows()
	{
		var deck1 = new List<CardValue>
		{
			(CardValue)25
		};

		var deck2 = new List<CardValue>
		{
			CardValue.Two
		};

		var input1 = new CardWarInputData(deck1, deck2);
		var input2 = new CardWarInputData(deck2, deck1);

		void Act() => _cardWarProcessor.ProcessInput(input1);
		void Act2() => _cardWarProcessor.ProcessInput(input2);

		Assert.Throws(Is.InstanceOf<Exception>(), Act);
		Assert.Throws(Is.InstanceOf<Exception>(), Act2);
	}

	private MirroredResult GetResultFromInput(
		IReadOnlyCollection<CardValue> deck1,
		IReadOnlyCollection<CardValue> deck2
	)
	{
		var input1 = new CardWarInputData(deck1, deck2);
		var input2 = new CardWarInputData(deck2, deck1);

		var result1 = _cardWarProcessor.ProcessInput(input1);
		var result2 = _cardWarProcessor.ProcessInput(input2);
		return new MirroredResult(result1, result2);
	}
}