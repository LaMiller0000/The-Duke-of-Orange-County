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
		var areaScene = GD.Load<PackedScene>("res://Test/Test Objects/area_3d.tscn");
		_areaNum = areaScene.Instantiate<AreaNumTest1>();
		Node3D t1 = GetNode<Node3D>("Target");
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
		_areaNum.QueueFree();
		
	}
}
