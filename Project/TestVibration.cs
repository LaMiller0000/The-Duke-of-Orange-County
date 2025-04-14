using Godot;
using System;

public partial class TestVibration : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	private void _on_button_button_down()
	{
		//Input.StartJoyVibration(0, 1, 1, 0);
		// Replace with function body.
	}
		
	private void _on_button_toggled(bool toggled_on)
	{
		if (toggled_on)
		{
			Input.StartJoyVibration(0, 1, 1, 0);
		}
		else
		{
			Input.StopJoyVibration(0);
		}
		
		// Replace with function body.
	}
}


