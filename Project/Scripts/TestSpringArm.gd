extends Node3D

@export var sensitivity: int = 10

func _ready() -> void:
	set_as_top_level(true)

func _unhandled_input(event : InputEvent) -> void:
	if event.is_action("ui_left"):
		rotation_degrees.y += sensitivity
	elif event.is_action("ui_right"):
		rotation_degrees.y -= sensitivity
	else:
		rotation_degrees.y += 0
