using UnityEngine;

[System.Serializable]
public class Stat
{
	[SerializeField]
	[Range(-1f, 1f)]
	private float levelMultiplier = 1;

	/// <summary>
	/// Percentage that each level raises the base value
	/// </summary>
	public float LevelMultiplier
	{
		get { return LevelMultiplier; }
	}

	/// <summary>
	/// The current level of this stat
	/// </summary>
	public int Level { get; set; }

	[SerializeField]
	private float baseValue = 1;

	/// <summary>
	/// The value of this stat before any modifications
	/// </summary>
	public float BaseValue
	{
		get { return baseValue; }
		protected set { baseValue = value; }
	}

	/// <summary>
	/// Gets the current base value multiplier from the current level
	/// </summary>
	public float Multiplier { get { return 1f + levelMultiplier * Level; } }

	/// <summary>
	/// The current value for this stat
	/// </summary>
	public virtual float Value { get { return baseValue * Multiplier; } }

	public Stat(float levelMultiplier)
	{
		this.levelMultiplier = levelMultiplier;
	}
}
