using blackjack.blackjack;
using Godot;

namespace blackjack;
public partial class Card : TextureRect
{
	public void SetCard(string card)
	{
		var atlas = new AtlasTexture();
		atlas.Atlas = GD.Load<Texture2D>("res://assets/cards.png");
		atlas.Region = CardAtlas.GetRegion(card);
		Texture = atlas;
	}
}
