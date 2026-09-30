using Godot;
using Godot.Collections;

namespace blackjack.blackjack;

public static class CardAtlas
{
	public const int CardWidth = 70;
	private const int CardHeight = 98;
	private const int Gap = 1;
	private const int Margin = 2;

	//Top left pixel of each card on atlas sheet
	private static readonly Dictionary<string, Vector2I> Positions = new()
	{
		{ "Two",   new Vector2I(0, 0) },
		{ "Three", new Vector2I(1, 0) },
		{ "Four",  new Vector2I(2, 0) },
		{ "Five",  new Vector2I(3, 0) },
		{ "Six",   new Vector2I(4, 0) },
		{ "Seven", new Vector2I(0, 1) },
		{ "Eight", new Vector2I(1, 1) },
		{ "Nine",  new Vector2I(2, 1) },
		{ "Ten",   new Vector2I(3, 1) },
		{ "Jack",  new Vector2I(4, 1) },
		{ "Queen", new Vector2I(0, 2) },
		{ "King",  new Vector2I(1, 2) },
		{ "Ace",   new Vector2I(2, 2) },
		{ "Back",  new Vector2I(3, 2) },
	};

	public static Rect2 GetRegion(string card)
	{
		var cell = Positions[card];
		var x = Margin + cell.X * (CardWidth + Gap);
		var y = Margin + cell.Y * (CardHeight + Gap);
		return new Rect2(x, y, CardWidth, CardHeight);
	}
}
