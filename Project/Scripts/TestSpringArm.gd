extends Node3D

func _ready() -> void:
	set_as_top_level(true)
	


func _unhandled_input(event : InputEvent) -> void:
	if event.is_action_pressed("ui_left"):
		rotation_degrees.y += 10
	elif event.is_action_pressed("ui_right"):
		rotation_degrees.y -= 10
	else:
		rotation_degrees.y += 0
	#code below keeping till I know I can delete
	"""
	if event is InputEventMouseMotion:
		#rotation_degrees.x -= event.relative.y * mouse_sensitivity
		#rotation_degrees.x = clamp(rotation_degrees.x, -90.0, 30.0)
		
		rotation_degrees.y -= event.relative.x * 1.2
		rotation_degrees.y = wrapf(rotation_degrees.y, 0.0, 360.0)
"""
	"""
func _process(delta):
	if InputEventJoypadMotion:
		#rotation_degrees.x -= Input.get_joy_axis(0.07, JOY_AXIS_3) * 1.9 #+ mouse_sensitivity * 2
		#rotation_degrees.x = clamp(rotation_degrees.x, -90.0, 30.0)
		rotation_degrees.y -= Input.get_joy_axis(0.07, JOY_AXIS_2) * 1.9 #+ mouse_sensitivity * 2
		rotation_degrees.y = wrapf(rotation_degrees.y, 0.0, 360.0)
	else:
		rotation_degrees.z = 0
		#rotation_degrees.x = 0
		print("rotation = 0")
"""
