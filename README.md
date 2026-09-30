# BlackJack_Godot
Blackjack game built in GODOT using C#

Includes a working blackjack UI game scene, and a basic player character that can walk around in a separate scene.

Scenes have to be controlled manually currently, what is visible can be changed using the ShowWorld(), HideWorld(), ShowBlackJack() and HideBlackJack() functions in Main.cs

Player character can be controlled using 'WASD', and shift can be held to increase walking speed.

BlackJack player count can be changed by setting the playerCount: x, on line 13 of Blackjack.cs
(Anything above 5 and cards may start to render on top of each other after a few hits, but functionality is still the same, anything above 12 and cards will render on top of each other from the start)

All players are controlled using the 'Hit' and 'Stand' buttons in the centre of the screen, dealer follows the rules and will always hit until he has 16 or more.
