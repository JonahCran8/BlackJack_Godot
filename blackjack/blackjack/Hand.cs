using System.Collections.Generic;
using Godot;

namespace blackjack.blackjack;

public class Hand
{
    private readonly List<string> _cards = new();
    public IReadOnlyList<string> Cards => _cards;
    public bool Dealer = false;

    //Adds the cards to hand, argument will be fed by DealCard
    public void AddCard(string card)
    {
        _cards.Add(card);
    }

    //Returns the cards in hand
    public List<string> GetCards()
    {
        return _cards;
    }

    //Links the cards string to a value
    private static int CardValue(string card) => card switch
    {
        "Two" => 2, "Three" => 3, "Four" => 4, "Five" => 5,
        "Six" => 6, "Seven" => 7, "Eight" => 8, "Nine" => 9,
        "Ten" or "Jack" or "Queen" or "King" => 10,
        "Ace" => 11,
        _ => 0
    };

    //Calculates the value of a hand
    public int GetValues()
    {
        int total = 0, aces = 0;
        foreach (var card in _cards)
        {
            total += CardValue(card);
            if (card == "Ace") aces++;
        }
        while (total > 21 && aces > 0) { total -= 10; aces--; }
        return total;
    }
}