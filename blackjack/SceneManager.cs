using Godot;
using System;

public partial class SceneManager : Node
{
	[Export] private Node2D _world;
	[Export] private Node2D _blackjack;
	
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
}
