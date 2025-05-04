extends Node3D

@export var sensitivity: int = 50
var direction: int = 0  # Store the input direction

func _ready() -> void:
	set_as_top_level(true)

func _input(event : InputEvent) -> void:
	if Input.is_action_pressed("ui_left"):
		direction = -1  # Rotate left
	elif Input.is_action_pressed("ui_right"):
		direction = 1  # Rotate right
	else:
		direction = 0  # No rotation

func _process(delta: float) -> void:
	if direction != 0:
		rotation_degrees.y += (sensitivity * direction) * delta
