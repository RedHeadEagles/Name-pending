using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
	[SerializeField]
	private float max = 1;

	/// <summary>
	/// The maximum value for this stat
	/// </summary>
	public float Max
	{
		get { return max; }
		set
		{
			max = value;
			Current = current;
		}
	}

	[SerializeField]
	private float current;

	/// <summary>
	/// Gets or sets the current value for this stat
	/// </summary>
	public float Current
	{
		get { return current; }
		set { current = Mathf.Clamp(value, 0, Max); }
	}

	public float Percentage { get { return Current / Max; } }

	public Stat(float max)
	{
		this.max = max;
		current = max;
	}

	public void Reset()
	{
		current = max;
	}
}
