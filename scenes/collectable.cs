using Godot;
using System;

public partial class collectable : Area2D
{	
	private void _on_body_entered(Node2D body)
	{
		GD.Print(body.Name);
		if (body.Name == "MainCharacter")
		{
			QueueFree();
		}
	}
}
