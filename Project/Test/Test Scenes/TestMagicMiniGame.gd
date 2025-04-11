extends Node2D

@export var dot_scene: PackedScene  # Drag and drop your Dot scene here in the Inspector
@export var spawn_interval: float = 1.0  # Time between spawning dots
@export var spawn_area: Rect2  # Define an area where dots can appear

func _ready():
	$Timer.wait_time = spawn_interval
	$Timer.start()


func _on_timer_timeout():
	var dot = dot_scene.instantiate()  # Create an instance of the dot
	var random_position = Vector2(
		randi_range(spawn_area.position.x, spawn_area.position.x + spawn_area.size.x),
		randi_range(spawn_area.position.y, spawn_area.position.y + spawn_area.size.y)
	)
	dot.position = random_position
	add_child(dot)
	pass # Replace with function body.
