using Godot;
using System;
using Godot.NativeInterop;

public partial class AreaTargetSt : Node
{
	// Called when the node enters the scene tree for the first time.
	[Signal] public delegate void TargetAquiredEventHandler();
	private Area3D _area;
	public Node3D player;
	private CollisionShape3D _collisionShape;
	private int radius {get; set;}

	public AreaTargetSt(int radius)
	{
		this.radius = radius;
	}
	public override void _Ready()
	{
		GD.Print(1);
		_area = new Area3D();
		_collisionShape = new CollisionShape3D();
		_collisionShape.Shape = new SphereShape3D();
		//.AddChild(_area);
		_area.AddChild(_collisionShape);
		_area.Scale = new Vector3(1, 1, 1) * radius;
		ulong objId = _area.GetInstanceId();
		_area.SetScript(GD.Load<Script>("res://Test/Test Scenes/AreaNumTest1.cs"));
		_area = InstanceFromId(objId) as Area3D;
		//_area.Set("radius", radius);
		
		if (_area is Node signalEmitter && signalEmitter.HasSignal("TargetRecieved"))
		{
			GD.Print("2");
			signalEmitter.Connect("TargetRecieved", new Callable(this, "TargetReceivedFunction"));
		}
		else
		{
			GD.PrintErr("Emitter instance does not have the signal 'TargetRecieved'.");
		}
	}

	public void TargetReceivedFunction(Node3D target)
	{
		GD.Print("4");
		//GD.Print("Target Recieved : " + target.HasSignal("TargetRecieved"));
		EmitSignal("TargetAquired", target);
	}
}
