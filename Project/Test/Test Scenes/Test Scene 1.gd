extends Node3D
var target

func _ready():
	target = $Target

func _process(delta):
	
	pass

func _input(event):
	if Input.is_action_just_pressed("ui_accept"):
		if $Root.STATED == 1:
			$Root.STATED = $Root.state.TARGETING
			var queue_script = ResourceLoader.load("res://Test/Test Scripts/AreaTargetSt.cs")
			if queue_script:
				var queue = queue_script.instantiate()  # Use `instantiate()` in Godot 4.x
				$Root.add_child(queue)
			else:
				print("Failed to load script. Check path or script type.")
			$Root.TargetRotationSet()
		elif $Root.STATED == 0:
			$Root.STATED = $Root.state.FREECAM

func TargetAquiredF(t):
	print("FUCK")
	target = t
	pass

