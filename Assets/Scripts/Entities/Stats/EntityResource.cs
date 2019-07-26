using UnityEngine;

[System.Serializable]
public class EntityResource : Stat
{
	/// <summary>
	/// The maximum amount of this resource
	/// </summary>
	public float Max { get { return Value; } }
	
	private float current;

	/// <summary>
	/// The current amount of this resource
	/// </summary>
	public float Current
	{
		get { return current; }
		set { current = Mathf.Clamp(value, 0, Max); }
	}

	/// <summary>
	/// The current percent remaining
	/// </summary>
	public float Percentage { get { return Current / Max; } }

	public EntityResource(float levelModifier) : base(levelModifier)
	{
		Reset();
	}

	/// <summary>
	/// Makes the current value equal the maximum value
	/// </summary>
	public void Reset()
	{
		current = Max;
	}
}
