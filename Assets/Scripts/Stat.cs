using UnityEngine;

public class Stat
{
	private readonly float scale;

	private int level;

	/// <summary>
	/// The current level of this stat
	/// </summary>
	public int Level
	{
		set
		{
			level = value;
			Recalculate();
		}
	}

	private float current;

	/// <summary>
	/// The maximum amount of this stat
	/// </summary>
	public float Max { get; set; }

	/// <summary>
	/// The current amount of this stat remaining
	/// </summary>
	public float Current
	{
		get { return current; }
		set { current = Mathf.Clamp(value, 0, Max); }
	}

	/// <summary>
	/// The current percentage of this stat remaining
	/// </summary>
	public float Percent { get { return current / Max; } }

	private float baseValue;

	/// <summary>
	/// The base value of this stat
	/// </summary>
	public float Base
	{
		set
		{
			baseValue = value;
			Recalculate();
		}
	}

	public Stat(float scale)
	{
		this.scale = scale;
	}

	/// <summary>
	/// Recalculate what the Max value should be, performed automatically when changing level or base value
	/// </summary>
	public void Recalculate()
	{
		Max = baseValue * (1f + scale * level);
		current = Max;
	}

	/// <summary>
	/// Reset the current value to the max
	/// </summary>
	public void Reset()
	{
		current = Max;
	}
}
