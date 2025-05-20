using System;
using Godot;

public partial class TestingScene1 : Node3D
{
	// Reference to the node with GDScript
	private CharacterBody3D characterBody;
	//[Export] Node3D target = null;
	public Area3D _area;
	public Node3D player;
	private CollisionShape3D _collisionShape;
	private Script instance;
	[Export] public PackedScene scene;
	private AreaNumTest1 _areaNum;
	private int radius {get; set;}
	
	public override void _Ready()
	{
		radius = 10;
		characterBody = GetNode<CharacterBody3D>("Root");
		// Get the node with your GDScript attached
		/*
		_area = new Area3D();
		_collisionShape = new CollisionShape3D();
		_collisionShape.Shape = new SphereShape3D();
		MeshInstance3D mesh = new MeshInstance3D();
		mesh.Mesh = new SphereMesh();
		AddChild(_area);
		_area.AddChild(_collisionShape);
		_area.AddChild(mesh);
		_area.Scale = new Vector3(1, 1, 1) * radius;
		ulong objId = _area.GetInstanceId();
		_area.SetScript(GD.Load<Script>("res://Test/Test Scenes/AreaNumTest1.cs"));
		_area = InstanceFromId(objId) as Area3D;

		if (_area is Node signalEmitter && signalEmitter.HasSignal("TargetRecieved"))
		{
			GD.Print("2");
			signalEmitter.Connect("TargetRecieved", new Callable(this, "TargetReceivedFunction"));
		}
		else 
		{
			GD.PrintErr("Emitter instance does not have the signal 'TargetRecieved'.");
		}
		*/
	
		
	}
	
	public void TargetReceivedFunction(Node3D target)
	{
		if (target != null)
		{
			characterBody.Set("target", target);
			
			characterBody.Set("STATED", 0);
		}
		//characterBody.RemoveChild(_areaNum);
		_areaNum.QueueFree();
		
	}
	
	public override void _Input(InputEvent @event)
	{
		if (Input.IsActionJustReleased("ui_accept"))
		{
			characterBody = GetNode<CharacterBody3D>("Root");
			GD.Print("characterBody: ", characterBody.Get("STATED"));
			if ((int)characterBody.Get("STATED") == 1)
			{
				_areaNum = scene.Instantiate<AreaNumTest1>();
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
				//GD.Print(_area.GetMethodList());
				
			}
			else if ((int)characterBody.Get("STATED") == 0)
			{
				
				characterBody.Set("STATED", 1);
			}
		}
	}
}
