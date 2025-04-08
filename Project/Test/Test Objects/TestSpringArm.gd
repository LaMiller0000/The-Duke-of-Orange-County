extends Spatial

export var mouse_sensitivity := 0.05
var test = false
func _ready() -> void:
	set_as_toplevel(true)
	Input.set_mouse_mode(Input.MOUSE_MODE_CAPTURED)


func _unhandled_input(event : InputEvent) -> void:
	if event is InputEventMouseMotion:
		rotation_degrees.x -= event.relative.y * mouse_sensitivity
		rotation_degrees.x = clamp(rotation_degrees.x, -90.0, 30.0)
		
		rotation_degrees.y -= event.relative.x * mouse_sensitivity
		rotation_degrees.y = wrapf(rotation_degrees.y, 0.0, 360.0)
		
func _process(delta):
	if InputEventJoypadMotion:
		rotation_degrees.x -= Input.get_joy_axis(0.07, JOY_AXIS_3) * 1.9 #+ mouse_sensitivity * 2
		rotation_degrees.x = clamp(rotation_degrees.x, -90.0, 30.0)
		rotation_degrees.y -= Input.get_joy_axis(0.07, JOY_AXIS_2) * 1.9 #+ mouse_sensitivity * 2
		rotation_degrees.y = wrapf(rotation_degrees.y, 0.0, 360.0)
	else:
		rotation_degrees.z = 0
		rotation_degrees.x = 0
		print("rotation = 0")

