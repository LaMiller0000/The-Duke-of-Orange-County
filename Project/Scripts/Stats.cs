using Godot;
using System;
using System.Collections.Generic;

public partial class Stats : Node
{
	private List<pInfo> pStats = new List<pInfo>();
	private float confidence {  get; set; }


	public void addStat()
	{
		pStats.Add(new pInfo());
	}

	public void delStats()
	{
		pStats.Clear();
	}
	
	public double Deviation()
	{
		double x = 0;
		float x2 = 0;
		float xBar;
		for (int i = 0; i < pStats.Count; i++) 
		{
			x2 += pStats[i].playerHealth;
		}
		xBar = x2 / pStats.Count;
		for (int i = 0; i < pStats.Count; i++)
		{
			x += Math.Pow((pStats[i].playerHealth - xBar), 2f);
		}
		return Math.Sqrt(x / (pStats.Count - 1));
	}

	public double sError()
	{
		return (Deviation() / Math.Sqrt(pStats.Count));
	}

	public bool testStat(double sampleMean, double popMean)
	{
		double t = (sampleMean - popMean) / sError();
		if (pVal(t) < confidence) { return true; }
		else { return false; }
	}
	private double pVal(double z)
	{
		double p = 0.3275911;
		double a1 = 0.254829592;
		double a2 = -0.284496736;
		double a3 = 1.421413741;
		double a4 = -1.453152027;
		double a5 = 1.061405429;

		int sign;
		if (z < 0.0)
			sign = -1;
		else
			sign = 1;

		double x = Math.Abs(z) / Math.Sqrt(2.0);
		double t = 1.0 / (1.0 + p * x);
		double erf = 1.0 - (((((a5 * t + a4) * t) + a3) * t + a2) * t + a1) * t * Math.Exp(-x * x);
		return 0.5 * (1.0 + sign * erf);
	}
}
