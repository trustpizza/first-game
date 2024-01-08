using Godot;
using System;

public partial class main_character : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -450.0f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	private AnimatedSprite2D sprite2d;
	public override void _Ready()
	{
		sprite2d = GetNode<AnimatedSprite2D>("Sprite2D");
	}
 

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		// Animations

		if (Math.Abs(velocity.X) > 1)
		{
			GD.Print("Running");
			sprite2d.Animation = "running";
		}
		else
		{
			sprite2d.Animation = "idle";
		}
		// Add the gravity.
		if (!IsOnFloor()) 
		{
			velocity.Y += gravity * (float)delta;
			sprite2d.Animation = "jumping";
		}
		// int jumpCount = 0;
		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
			// jumpCount += 1;
			velocity.Y = JumpVelocity;
		// else if (Input.IsActionJustPressed("ui_accept") && jumpCount < 2)
		// 	velocity.Y = JumpVelocity;
		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();

		bool isLeft = velocity.X < 0;
		sprite2d.FlipH = isLeft;

	}
}
