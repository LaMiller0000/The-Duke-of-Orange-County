extends Node3D

func _process(delta):
	if Input.is_action_just_pressed("ui_accept") && $Root.STATED == 1:
		$Root.STATED = $Root.state.TARGETING
		$Root.TargetRotationSet()
	elif Input.is_action_just_pressed("ui_accept") && $Root.STATED == 0:
		$Root.STATED = $Root.state.FREECAM
	pass
