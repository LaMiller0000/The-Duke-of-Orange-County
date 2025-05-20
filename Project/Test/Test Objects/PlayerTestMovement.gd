extends CharacterBody3D

@onready var _spring_arm: SpringArm3D = $SpringArm3D
@onready var eyes: Node3D = $Eyes
@onready var RotationModel: Node3D = $CollisionShape3D
var gravity = 9.8
var counter = 0
@export var target: Node
@export var speed: int = 20
var _velocity := Vector3.ZERO
var target_position: Vector3 = Vector3.ZERO
var direction = global_position
enum state {TARGETING, FREECAM, ROLLING}
@export var STATED = state.FREECAM
var PREVSTATE = state.FREECAM
var rolling: bool = false

func _physics_process(_delta: float) -> void:
	if Input.is_action_just_released("roll"):
		counter = 0
		if _velocity == Vector3(0, _velocity.y, 0):
			print(_velocity)
			target_position = global_position + RotationModel.basis.z * 10
			pass
		else:
			print(_velocity.rotated(Vector3.UP, _spring_arm.rotation.y).normalized())
			target_position = Vector3(global_position.x + (_velocity.x * 10.0), 0.0, global_position.z + (_velocity.z *10.0))
		rolling = true
	if rolling == true:
		counter += 1
		direction = global_position.direction_to(target_position)
		_velocity = direction
		if global_position.distance_to(target_position) < 1.1 || counter > 24:
			rolling = false
	elif rolling == false:
		_velocity.x = Input.get_action_strength("right") - Input.get_action_strength("left")
		_velocity.z = Input.get_action_strength("down") - Input.get_action_strength("up")
		match STATED:
			state.FREECAM:
				_velocity = _velocity.rotated(Vector3.UP, _spring_arm.rotation.y).normalized()
			state.TARGETING:
				if target == null:
					STATED = state.FREECAM
				else:
					eyes.look_at(target.global_transform.origin, Vector3.DOWN)
					_spring_arm.rotation.y = eyes.rotation.y
					_velocity = _velocity.rotated(Vector3.UP, eyes.rotation.y).normalized()
			_:
				print("NOTHING")
	if _velocity != Vector3(0, _velocity.y, 0):
		RotationModel.rotation.y = Vector2(velocity.z, velocity.x).angle()
	_velocity.y -= gravity * _delta
	set_velocity(_velocity * speed)
	move_and_slide()
	_spring_arm.position = position

func TargetRotationSet():
	RotationModel.rotation.y = 3.14
