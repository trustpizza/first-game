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
 
	private int jumpCount = 2;
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		// Animations

		if (Math.Abs(velocity.X) > 1)
		{
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

		if (Input.IsActionJustPressed("jump"))
		{
			GD.Print(jumpCount);
			jumpCount -= 1;
			
			if (jumpCount > 0)
			{
				velocity.Y = JumpVelocity;
			}
		}
		//if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		//{
			//velocity.Y = JumpVelocity;
			//jumpCount -= 1;
			//GD.Print(jumpCount);
		//}

		if (IsOnFloor())
		{
			jumpCount = 2;
		}
		
		
		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		float direction = Input.GetAxis("left", "right");
		if (direction != 0)
		{
			velocity.X = direction * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, 12);
		}

		Velocity = velocity;
		MoveAndSlide();

		bool isLeft = velocity.X < 0;
		sprite2d.FlipH = isLeft;

	}
}
