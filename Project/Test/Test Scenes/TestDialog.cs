using Godot;
using System;

public partial class TestDialog : Node2D
{
	
	private int talkCount = 0;
	private int charCount = 0;
	private string test;
	private float _waitTime;
	private int _fontSize;
	private Vector2 _textPosition;
	
	[Export] private int maxCount;
	[Export] public string[] textArray = { "this is a test", "this is a second test", "this is the third test", "this is the last test" };

	private Panel panel;
	private RichTextLabel text;
	private Timer timer;

	public TestDialog()
	{
		_waitTime = 0.1f;
	}
	
	public TestDialog(float waitTime, string[] _textArray, Vector2 _position, int _fontSize)
	{
		_waitTime = waitTime;
		textArray = _textArray;
		_textPosition = _position;
		this._fontSize = _fontSize;
	}
	
	public override void _Ready()
	{
		maxCount = textArray.Length;
		panel = new Panel();
		text = new RichTextLabel();
		panel.Size = _textPosition;
		panel.Position = _textPosition;
		AddChild(panel);
		text.Size = panel.Size;
		panel.AddChild(text);
		timer = new Timer();
		timer.WaitTime = _waitTime;
		AddChild(timer);
		SetTextArrayCount();
		timer.Timeout += OnTimerTimeout;
		panel.Visible = true;
		text.Visible = true;
		Theme theme = new Theme();
		theme.DefaultFontSize = _fontSize;
		text.Theme = theme;
		text.ScrollFollowing = true;
	}

	private void SetTextArrayCount()
	{
		if (!timer.IsStopped())
		{
			timer.Stop();
			text.Text += " =>";
			charCount = 0;
			talkCount++;
		}
		
		test = textArray[talkCount];
	}

	private void OnTimerTimeout()
	{
		text.Text += test[charCount];
		charCount++;
		if (charCount >= test.Length)
		{

			SetTextArrayCount();
		}
	}
	
	public override void _Input(InputEvent @event)
	{
		if (Input.IsActionJustPressed("ui_accept"))
		{
			if (timer.IsStopped())
			{
				if (talkCount < maxCount)
				{
					timer.Start();
				}
				if (talkCount != 0)
				{
					text.Text = text.Text.Replace(" =>", "");
					text.Text += "\n";
				}	
			}
			else
			{
				for (int i = charCount; i < test.Length; i++)
				{
					text.Text += test[i];
				}
				SetTextArrayCount();
			}
		}
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
