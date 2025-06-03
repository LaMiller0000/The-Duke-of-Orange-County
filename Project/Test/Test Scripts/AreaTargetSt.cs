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
	private double _maxAngle = 5 * Math.PI / 6;//(5 * Math.PI) / 6;
	private double _minAngle = Math.PI / 6 ;//Math.PI / 6;
	private double _leftMaxAngle = Math.PI;
	private double _rightMinAngle = Math.PI;
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
		if (Input.IsActionJustReleased("ui_accept"))
		{
			GD.Print("Player State = " + characterBody.Get("STATED"));
			if ((int)characterBody.Get("STATED") == 1)
			{
				_target = null;
				NormalTest();
				//characterBody.Set("STATED", 0);
			}
			else if ((int)characterBody.Get("STATED") == 0)
			{
				characterBody.Set("STATED", 1);
			}
		}
		else if (Input.IsActionJustReleased("ui_left") && (int)characterBody.Get("STATED") == 0 && _target != null)
		{
			GD.Print("Left Test");
			LeftTest();
		}
		else if (Input.IsActionJustReleased("ui_right") && (int)characterBody.Get("STATED") == 0 && _target != null)
		{
			GD.Print("Right Test");
			RightTest();			
		}
		
	}

	public void NormalTest()
	{
		MakeNumTest(null, _minAngle, _maxAngle);
		GD.Print("Normal Test");
		GD.Print("Angle = " + characterBody.Call("GetSpringArmRotation"));
	}

	public void LeftTest()
	{
		//MakeNumTest(_target, (2 * Math.PI)/3, _maxAngle);
		//MakeNumTest(null, _minAngle + characterBody.GlobalRotation.Y, _maxAngle + characterBody.GlobalRotation.Y);	
	}

	public void RightTest()
	{
		MakeNumTest(_target, _minAngle + (double)characterBody.Get("GetSpringArmRotation()"), _rightMinAngle + (double)characterBody.Get("GetSpringArmRotation()") );
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
