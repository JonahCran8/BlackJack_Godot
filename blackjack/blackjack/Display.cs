using System;
using System.Collections.Generic;
using Godot;

namespace blackjack.blackjack;

public partial class Display : Control
{
	[Export] private PackedScene _cardScene;
	private Vector2 _screenSize;
	private readonly List<HBoxContainer> _playerContainers = new();
	private HBoxContainer _dealerContainer;

	public override void _Ready()
	{
		_screenSize = GetViewport().GetVisibleRect().Size;
	}
	
	public void DisplayPlayerHands(IReadOnlyList<Hand> players)
	{
		foreach (var container in _playerContainers)
		{
			container.QueueFree();
		}
		_playerContainers.Clear();
		for (var p = 0; p < players.Count; p++)
		{
			var container = new HBoxContainer();
			_playerContainers.Add(container);
			container.Alignment = BoxContainer.AlignmentMode.Center;
			var middle = (players.Count - 1) / 2f;
			var yChange = Math.Abs(p - middle) * 20;
			AddChild(container);
			var cards = players[p].GetCards();
			for (var i = 0; i < players[p].GetCards().Count; i++)
			{
				DisplayCard(cards[i], container, i, players.Count);
			}
			container.ResetSize();
			var target = new Vector2(_screenSize.X / (players.Count + 1) * (p + 1), _screenSize.Y * 0.75f - yChange);
			container.Position = target - new Vector2(container.Size.X / 2,  0);
		}
	}

	public void DisplayDealerHand(Hand dealer, bool hideFirstCard)
	{
		_dealerContainer?.QueueFree();
		_dealerContainer = new HBoxContainer();
		_dealerContainer.Alignment = BoxContainer.AlignmentMode.Center;
		AddChild(_dealerContainer);

		var cards = dealer.GetCards();

		for (var i = 0; i < cards.Count; i++)
		{
			var cardName = hideFirstCard && i == 0
				? "Back"
				: cards[i];

			DisplayCard(cardName, _dealerContainer, i, cards.Count);
		}

		_dealerContainer.ResetSize();

		var target = new Vector2(
			_screenSize.X / 2f,
			_screenSize.Y * 0.1f
		);

		_dealerContainer.Position = target - new Vector2(_dealerContainer.Size.X / 2, 0);
	}

	private void DisplayCard(string cardName, HBoxContainer container, int index, int cardCount)
	{
		var card = _cardScene.Instantiate<Card>();
		container.AddChild(card);
		card.SetCard(cardName);
	}

	public (Button hitButton, Button standButton) DisplayButtons()
	{
		var buttonBox = new HBoxContainer();
		buttonBox.Alignment = BoxContainer.AlignmentMode.Center;
		AddChild(buttonBox);
		
		var hitButton = DisplayHitButton();
		var standButton = DisplayStandButton();
		
		buttonBox.AddChild(hitButton);
		buttonBox.AddChild(standButton);
		buttonBox.ResetSize();
		var target = new Vector2(_screenSize.X / 2f, _screenSize.Y * 0.70f - buttonBox.Size.Y / 2);
		buttonBox.Position = target -  new Vector2(buttonBox.Size.X / 2, 0);

		return (hitButton, standButton);

	}

	public Button DisplayHitButton()
	{
		var button = new Button();
		button.Text = "Hit";
		button.Size =  new Vector2(_screenSize.X / 10,  button.Size.Y);
		return button;
	}

	public Button DisplayStandButton()
	{
		var button = new Button();
		button.Text = "Stand";
		button.Size =  new Vector2(_screenSize.X / 10,  button.Size.Y);
		return button;
	}
	
}
