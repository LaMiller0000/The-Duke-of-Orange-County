using Godot;
using System;

public partial class PlayerTestMovement : CharacterBody3D
{
	private SpringArm3D springArm;
	private Node3D eyes;
	private Node3D rotationModel;
	private float gravity = 9.8f;
	private int counter = 0;
	
	public Node3D target {get; set;}
	
	[Export]
	public int speed = 20;
	
	private Vector3 _velocity = Vector3.Zero;
	private Vector3 targetPosition = Vector3.Zero;
	private Vector3 direction;
	
	public enum State { TARGETING, FREECAM, ROLLING }
	
	[Export]
	public State STATED = State.FREECAM;
	
	private State PREVSTATE = State.FREECAM;
	private bool rolling = false;

	public override void _Ready()
	{
		springArm = GetNode<SpringArm3D>("SpringArm3D");
		eyes = GetNode<Node3D>("Eyes");
		rotationModel = GetNode<Node3D>("CollisionShape3D");
		direction = GlobalPosition;
	}

	public override void _PhysicsProcess(double delta)
	{
		if (Input.IsActionJustReleased("roll"))
		{
			counter = 0;
			if (_velocity == new Vector3(0, _velocity.Y, 0))
			{
				GD.Print(_velocity);
				targetPosition = GlobalPosition + rotationModel.Basis.Z * 10;
			}
			else
			{
				GD.Print(_velocity.Rotated(Vector3.Up, springArm.Rotation.Y).Normalized());
				targetPosition = new Vector3(
					GlobalPosition.X + (_velocity.X * 10.0f), 
					0.0f, 
					GlobalPosition.Z + (_velocity.Z * 10.0f)
				);
			}
			rolling = true;
		}

		if (rolling == true)
		{
			counter++;
			direction = GlobalPosition.DirectionTo(targetPosition);
			_velocity = direction;
			if (GlobalPosition.DistanceTo(targetPosition) < 1.1f || counter > 24)
			{
				rolling = false;
			}
		}
		else if (rolling == false)
		{
			_velocity.X = Input.GetActionStrength("right") - Input.GetActionStrength("left");
			_velocity.Z = Input.GetActionStrength("down") - Input.GetActionStrength("up");

			switch (STATED)
			{
				case State.FREECAM:
					_velocity = _velocity.Rotated(Vector3.Up, springArm.Rotation.Y).Normalized();
					break;
				case State.TARGETING:
					if (target == null)
					{
						//STATED = State.FREECAM;
					}
					else
					{
						eyes.LookAt(target.GlobalPosition, Vector3.Down);
						springArm.Rotation = new Vector3(springArm.Rotation.X, eyes.Rotation.Y, springArm.Rotation.Z);
						_velocity = _velocity.Rotated(Vector3.Up, eyes.Rotation.Y).Normalized();
					}
					break;
				default:
					GD.Print("NOTHING");
					break;
			}
		}

		if (_velocity != new Vector3(0, _velocity.Y, 0))
		{
			rotationModel.Rotation = new Vector3(
				rotationModel.Rotation.X,
				Mathf.Atan2(_velocity.X, _velocity.Z),
				rotationModel.Rotation.Z
			);
		}

		_velocity.Y -= gravity * (float)delta;
		Velocity = _velocity * speed;
		MoveAndSlide();
		springArm.Position = Position;
	}

	public void TargetRotationSet()
	{
		rotationModel.Rotation = new Vector3(
			rotationModel.Rotation.X,
			3.14f,
			rotationModel.Rotation.Z
		);
	}
}
