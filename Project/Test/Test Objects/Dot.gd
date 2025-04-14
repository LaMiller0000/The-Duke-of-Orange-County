extends Area2D

signal MySignal

func emit_signal_message():
	emit_signal("MySignal")

func _on_mouse_entered():
	emit_signal_message()
	queue_free()

func _on_body_entered(body):
	if body is Node:
		#emit_signal_message()
		pass
