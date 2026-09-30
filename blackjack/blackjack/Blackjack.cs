using blackjack.blackjack;
using Godot;

namespace blackjack.blackjack;

public partial class Blackjack : Node2D
{
	[Export] private Display _display;
	private static Controller _controller;
	
	public override void _Ready()
	{
		_controller = new Controller(_display,  playerCount: 1);
		_display.DisplayPlayerHands(_controller.PlayerHands);
		_display.DisplayDealerHand(_controller.DealerHand, true);
	}
}
