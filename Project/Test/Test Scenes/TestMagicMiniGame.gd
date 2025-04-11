extends Control
@onready var window = $Panel
#Transform2D size = null

# Called when the node enters the scene tree for the first time.
func _ready():
	
	window.size = get_viewport_rect().size / 3
	window.set_position(Vector2(get_viewport_rect().size.x - window.size.x * 2, (get_viewport_rect().size.y + window.size.y) / 2))
	#window.position.x = get_viewport_rect().size / 2 - window.size.y
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass


func _on_node_2d_tree_entered(extra_arg_0):
	print(extra_arg_0)
	$Node2D.queue_free()
	pass # Replace with function body.
