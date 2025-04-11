extends Area2D

func _on_Area2D_mouse_entered():
	queue_free()  # This makes the dot disappear when the mouse hovers over it


func _on_mouse_entered():
	queue_free()
	pass # Replace with function body.

func _on_body_entered(body):
	queue_free()
	if body is RigidBody2D:
		queue_free()
	pass # Replace with function body.
