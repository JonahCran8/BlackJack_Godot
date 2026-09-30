using Godot;
using System;

public partial class Main : Node2D
{
	private Node _world;
	private Node _blackjack;
	
	public override void _Ready()
	{
		_world = GetNode("World");
		_blackjack = GetNode("Blackjack");
		
		ShowWorld();
		ShowBlackJack();
	}
	
	//Show and enable the world scene
	public void ShowWorld()
	{	
		_world.ProcessMode = ProcessModeEnum.Inherit;
		((CanvasItem)_world).Visible = true;
	}
	
	//Hide and disable the world scene
	public void HideWorld()
	{
		_world.ProcessMode = ProcessModeEnum.Disabled;
		((CanvasItem)_world).Visible = false;
	}
	
	//Show and enable the blackjack scene
	public void ShowBlackJack()
	{
		_blackjack.ProcessMode = ProcessModeEnum.Inherit;
		((CanvasItem)_blackjack).Visible = true;
	}
	
	//Hide and disable the blackjack scene
	public void HideBlackJack()
	{
		_blackjack.ProcessMode = ProcessModeEnum.Disabled;
		((CanvasItem)_blackjack).Visible = false;
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
}
