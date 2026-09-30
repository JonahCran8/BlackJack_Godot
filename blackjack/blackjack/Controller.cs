using System.Collections.Generic;
using System.IO;
using Godot;
using Godot.NativeInterop;

namespace blackjack.blackjack;

public class Controller
{
	private readonly List<Hand> _playerHands;
	private int _currentPlayerIndex = 0;
	private readonly int _playerCount;
	private const int DealRounds = 2;
	private readonly Deck _deck = new();
	private readonly Hand _dealerHand = new();
	private Display _display;

	public IReadOnlyList<Hand> PlayerHands => _playerHands;
	public Hand DealerHand => _dealerHand;
	
	public Controller(Display display, int playerCount)
	{
		_display = display;
		_playerCount = playerCount;
		_playerHands = new List<Hand>();
		_dealerHand.Dealer = true;
		StartRound();
	}
	
	private void AddPlayers()
	{
		for (var i = 0; i < _playerCount; i++)
		{
			_playerHands.Add(new Hand());
		}
	}

	private void StartingDeal()
	{
		for (var i = 0; i < DealRounds; i++)
		{
			_dealerHand.AddCard(_deck.DealCard());
			foreach (var hand in _playerHands)
			{
				hand.AddCard(_deck.DealCard());
			}
		}

	}
	
	private void StartRound()
	{
		AddPlayers();
		StartingDeal();
		if (GetPlayerState(_dealerHand) == PlayerState.Blackjack)
		{
			return;
		}
		TakeTurns();
	}

	private void TakeTurns()
	{
		PlayerTurn(_playerHands[_currentPlayerIndex]);
	}

	public PlayerState GetPlayerState(Hand hand)
	{
		var state = PlayerState.Null;

		var value = hand.GetValues();
		var cards = hand.GetCards();

		if (cards.Count == 2 && cards.Contains("Ace") && (cards.Contains("King") || cards.Contains("Queen") || cards.Contains("Jack") ||
									  cards.Contains("Ten")))
		{
			state = PlayerState.Blackjack;
		}

		else
		{
			switch (value)
			{
				case > 21:
					state = PlayerState.Bust;
					break;
				case < 21:
					state = PlayerState.HitOrStand;
					break;
				default:
					state = PlayerState.Stand;
					break;
			}
		}
		return state;
	}

	private void PlayerTurn(Hand hand)
	{
		var state = GetPlayerState(hand);

		
		var (hitButton, standButton) = _display.DisplayButtons();
		GD.Print("Buttons created");
		if (state == PlayerState.Blackjack)
		{
			hitButton.Disabled = true;
			EndPlayerTurn();
			return;
		}
		hitButton.Pressed += () =>
		{
			HitPressed(hand);
			if (GetPlayerState(hand) == PlayerState.Stand || GetPlayerState(hand) == PlayerState.Bust)
			{
				hitButton.Disabled = true;
				EndPlayerTurn();
			}
		};
		standButton.Pressed += () =>
		{
			hitButton.Disabled = true;
			EndPlayerTurn();
		};
	}

	private void DealerTurn(Hand hand)
	{
		_display.DisplayDealerHand(hand, false);

		if (hand.GetValues() >= 17)
		{
			EndRound();
			return;
		}

		hand.AddCard(_deck.DealCard());
		DealerTurn(hand);
	}

	private void HitPressed(Hand hand)
	{
		hand.AddCard(_deck.DealCard());
		_display.DisplayPlayerHands(PlayerHands);
	}

	private void EndPlayerTurn()
	{
		_currentPlayerIndex++;

		if (_currentPlayerIndex < _playerHands.Count)
		{
			PlayerTurn(_playerHands[_currentPlayerIndex]);
		}
		else
		{
			DealerTurn(_dealerHand);
		}
	}

	private void EndRound()
	{
		_display.DisplayDealerHand(_dealerHand, false);

		var dealerValue = _dealerHand.GetValues();
		var dealerBust = dealerValue > 21;

		foreach (var hand in _playerHands)
		{
			var playerValue = hand.GetValues();

			if (playerValue > 21)
			{
				GD.Print("Player loses - bust");
			}
			else if (dealerBust)
			{
				GD.Print("Player wins - dealer bust");
			}
			else if (playerValue > dealerValue)
			{
				GD.Print("Player wins");
			}
			else if (playerValue < dealerValue)
			{
				GD.Print("Player loses");
			}
			else
			{
				GD.Print("Push");
			}
		}
	}
}
