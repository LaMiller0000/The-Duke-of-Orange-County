extends Node2D

const MOUSE_SENSITIVITY = 700
var RADIUS = 200
var center = Vector2.ZERO
var viewport_size = Vector2.ZERO

func _ready():
	get_viewport().connect("size_changed", Callable(self, "_on_resize"))
	center = Vector2(viewport_size.x - (viewport_size.x / 3), viewport_size.y - (viewport_size.y / 3))
	RADIUS = viewport_size.x / 15
	_on_resize()

func _on_resize():
	viewport_size = get_viewport().size
	print("ON RESIZE")
	center = Vector2(viewport_size.x - (viewport_size.x / 3), viewport_size.y - (viewport_size.y / 3))
	RADIUS = viewport_size.x / 15 + 10

func _process(delta):
	var direction = Vector2(
		Input.get_action_strength("cursor_right") - Input.get_action_strength("cursor_left"),
		Input.get_action_strength("cursor_down") - Input.get_action_strength("cursor_up")
	)

	if direction.length() > 0:
		direction = direction.normalized()
		var movement = MOUSE_SENSITIVITY * direction * delta
		var new_position = get_global_mouse_position() + movement
		var offset = new_position - center
		if offset.length() > RADIUS:
			new_position = center
		get_viewport().warp_mouse(new_position)

