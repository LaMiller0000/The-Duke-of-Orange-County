using Godot;
using System;
using Godot.NativeInterop;

public partial class AreaTargetSt : Node
{
	// Called when the node enters the scene tree for the first time.
	[Signal] public delegate void TargetAquiredEventHandler();
	private Area3D _area;
	private CharacterBody3D characterBody;
	private CollisionShape3D _collisionShape;
	[Export] public PackedScene scene;
	private AreaNumTest1 _areaNum;
	private Node3D _target;
	private double _maxAngle = Math.PI;//(5 * Math.PI) / 6;
	private double _minAngle = 0;//Math.PI / 6;
	private double _leftMaxAngle = Math.PI / 2;
	private double _rightMinAngle = Math.PI / 2;
	private int radius {get; set;}

	public AreaTargetSt()
	{
		
	}

	public AreaTargetSt(int radius)
	{
		this.radius = radius;
	}

	public void setPlayer(CharacterBody3D _characterBody)
	{
		characterBody = _characterBody;
	}
	public override void _Ready()
	{
		
	}

	public override void _Input(InputEvent @event)
	{
		int currentState = (int)characterBody.Get("STATED");
	
		if (Input.IsActionJustReleased("ui_accept"))
		{
			if (currentState == 1)
			{
				// Toggle from state 1 back to state 0
				GD.Print("Player State = " + currentState + " -> Toggling to 0");
				characterBody.Set("STATED", 0);
				_target = null;
				NormalTest();
			}
			else if (currentState == 0)
			{
				// Toggle from state 0 to state 1
				GD.Print("Player State = " + currentState + " -> Toggling to 1");
				characterBody.Set("STATED", 1);
				DeleteTest();
			}
		}
		else if (Input.IsActionJustReleased("ui_left") && currentState == 0 && _target != null)
		{
			GD.Print("Left Test");
			LeftTest();
		}
		else if (Input.IsActionJustReleased("ui_right") && currentState == 0 && _target != null)
		{
			GD.Print("Right Test");
			RightTest();         
		}
	}

	public void DeleteTest()
	{
		_areaNum.QueueFree();
	}
	public void NormalTest()
	{
		MakeNumTest(null, _minAngle, _maxAngle);
		GD.Print("Normal Test");
		GD.Print("Angle = " + characterBody.Call("GetSpringArmRotation"));
	}

	public void LeftTest()
	{
		MakeNumTest(_target, _leftMaxAngle, _maxAngle);
		//MakeNumTest(null, _minAngle + characterBody.GlobalRotation.Y, _maxAngle + characterBody.GlobalRotation.Y);	
	}

	public void RightTest()
	{
		MakeNumTest(_target, _minAngle, _leftMaxAngle);
	}

	private void MakeNumTest(Node3D _target, double _minAngle, double _maxAngle)
	{
		var areaScene = GD.Load<PackedScene>("res://Test/Test Objects/area_3d.tscn");
		_areaNum = areaScene.Instantiate<AreaNumTest1>();
		Node3D t1 = GetNode<Node3D>("Target");
		_areaNum.setAngles(_minAngle, _maxAngle);
		_areaNum.setTarget(_target);
		_areaNum.setSpringArmRotation((double)characterBody.Call("GetSpringArmRotation"));
		characterBody.AddChild(_areaNum);

		_areaNum.radius = radius;
		GD.Print("target : " + _areaNum.getTarget());
		if (_areaNum is Node signalEmitter && signalEmitter.HasSignal("TargetRecieved"))
		{
			signalEmitter.Connect("TargetRecieved", new Callable(this, "TargetReceivedFunction"));
		}
		else
		{
			GD.PrintErr("Emitter instance does not have the signal 'OutOfPoints'.");
		}
		characterBody.Set("target", _areaNum.getTarget());
	}

	public void TargetReceivedFunction(Node3D target)
	{
		if (target != null)
		{
			characterBody.Set("target", target);
			characterBody.Set("STATED", 0);
		}
		_target = target;
		_areaNum.QueueFree();
	}
}
