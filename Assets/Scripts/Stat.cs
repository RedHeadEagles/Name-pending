using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
	[SerializeField]
	protected int baseValue = 1;

	/// <summary>
	/// Gets the base value for this stat
	/// </summary>
	public int BaseValue { get { return baseValue; } }

	/// <summary>
	/// The maximum value for this stat
	/// </summary>
	public int Max { get; set; }

	/// <summary>
	/// Gets or sets the current value for this stat
	/// </summary>
	public int Current { get; set; }

	public float Percentage { get { return (float)Current / Max; } }

	public Stat(int baseValue)
	{
		this.baseValue = baseValue;
		Max = baseValue;
		Current = baseValue;
	}

	/// <summary>
	/// Returns the Current and Max to their base value
	/// </summary>
	public void Reset()
	{
		Max = baseValue;
		Current = baseValue;
	}
}
