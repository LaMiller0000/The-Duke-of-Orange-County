extends Node3D

@export var sensitivity: int = 10
# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	
	if Input.is_action_pressed("ui_left"):
		rotation_degrees.y += sensitivity
	elif Input.is_action_pressed("ui_right"):
		rotation_degrees.y -= sensitivity
	else:
		rotation_degrees.y += 0
	pass
