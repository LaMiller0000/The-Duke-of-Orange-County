using Godot;
using System;

public partial class MousePosition : Node2D
{
	private const float MOUSE_SENSITIVITY = 700;
	private float RADIUS = 200f;
	private Vector2 center = Vector2.Zero;
	private Vector2 viewportSize = Vector2.Zero;

	public override void _Ready()
	{
		GetViewport().Connect("size_changed", new Callable(this, nameof(OnResize))); 
		OnResize(); // Initialize size and center
	}
	private void OnResize()
	{
		viewportSize = GetViewport().GetVisibleRect().Size;
		center = new Vector2(viewportSize.X - (viewportSize.X / 3), viewportSize.Y - (viewportSize.Y / 3));
		RADIUS = viewportSize.X / 15;

		GD.Print("ON RESIZE");
	}
	public void Center()
	{
		GetViewport().WarpMouse(center);
	}
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 direction = new Vector2(
			Input.GetActionStrength("cursor_right") - Input.GetActionStrength("cursor_left"),
			Input.GetActionStrength("cursor_down") - Input.GetActionStrength("cursor_up")
		);

		if (direction.Length() > 0)
		{
			direction = direction.Normalized();
			Vector2 movement = MOUSE_SENSITIVITY * direction * (float)delta;
			Vector2 newPosition = GetGlobalMousePosition() + movement;
			Vector2 offset = newPosition - center;

			if (offset.Length() > RADIUS)
			{
				newPosition = center + offset.Normalized() * RADIUS;
			}

			GetViewport().WarpMouse(newPosition);
		}
	}

}
