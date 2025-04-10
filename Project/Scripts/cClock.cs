using Godot;
using System;
using System.Diagnostics;

public partial class cClock : Node
{
    private Timer _timer;
    private int time {  get; set; }

    public cClock() => time = 0;

    public cClock(int time)
    {
        _timer = new Timer();
        this.time = time;
    }

    public int getTime() { return time; }
    public void setTime(int i = 0) => time = i;

    public void startClock() => _timer.Start(time);

    public void stopClock() => _timer.Stop();

    private void active()
    {
        while (true)
        {
            if (_timer.TimeLeft > 0 || _timer.IsStopped()) 
            {
                
            }
            else { break; }
        }
    }

}
