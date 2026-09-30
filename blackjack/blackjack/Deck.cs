using System;
using Godot;

namespace blackjack.blackjack;

public class Deck
{
	//Each card that will go in the deck, initialise variables
	private static readonly string[] Cards = ["Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Jack", "Queen", "King", "Ace"];
	private const int DeckConstant = 4;
	private readonly string[] _deck;
	private int _topCard;

	//Creates a deck instance using any amount of decks, defaults to 1
	public Deck(int deckCount = 1)
	{
		_deck = new string[Cards.Length * DeckConstant * deckCount];
		_topCard = _deck.Length - 1;
		BuildDeck();
		ShuffleDeck();
	}
	
	// Adds cards to the deck
	private void BuildDeck()
	{
		for (int i = 0; i < _deck.Length; i++)
		{
			_deck[i] = Cards[i %Cards.Length];
		}
	}

	//Fisher Yates shuffle
	private void ShuffleDeck()
	{
		GD.Print("Shuffled deck...\n");
		Random rand = new Random();
		for (int i = _deck.Length - 1; i > 0; i--)
		{
			var temp = _deck[i];
			var j = rand.Next(i + 1);
			_deck[i] = _deck[j];
			_deck[j] = temp;
		}
	}
	
	//Controls the decks status for dealing, tells AddCard which card to add
	public string DealCard()
	{
		if (_topCard < 0) return null;
		_topCard--;
		return _deck[_topCard];
	}

	//Temporary function to display the deck
	public void DisplayDeck()
	{
		GD.Print($"Shuffled deck:");
		foreach (var card in _deck)
		{
			GD.Print($"{card}");
		}
	}
}
