using Godot;

namespace blackjack.player;

public partial class Player : CharacterBody2D
{
	private AnimatedSprite2D _sprite;
	private const float Speed = 75f;

	public override void _Ready()
	{
		_sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	public override void _PhysicsProcess(double delta)
	{
		//Assigns direction that increases or decreases coordinates. starting 0,0 top left
		Vector2 direction = Input.GetVector(
			"move_left", //-x
			"move_right", //x
			"move_up", //-y
			"move_down" //y
		);

		Velocity = direction * Speed;
		_sprite.SpeedScale = 1f;
		if (Input.IsActionPressed("walk_faster")) 
		{
			Velocity = Velocity * 2;
			_sprite.SpeedScale = 1.5f;
		}
		MoveAndSlide();

		if (direction.Y < 0)
			_sprite.Play("up");
		else if (direction.Y > 0)
			_sprite.Play("down");
		else if (direction.X > 0)
			_sprite.Play("right");
		else if (direction.X < 0)
			_sprite.Play("left");
		else
			_sprite.Stop();
	}
}
