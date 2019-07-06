using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Health
{
	[Range(0, 1000)]
	[SerializeField]
	[Tooltip("The base HP of this entity")]
	private int baseHealth = 100;

	[SerializeField]
	private int max;

	/// <summary>
	/// The maximum amount of hp for this entity
	/// </summary>
	public int Max
	{
		get { return max; }
		set { max = value; }
	}

	[SerializeField]
	private int current;

	/// <summary>
	/// The current amount of hp remaining
	/// </summary>
	public int Current
	{
		get { return current; }
		set { current = value; }
	}

	/// <summary>
	/// Returns true if this entity has zero health left
	/// </summary>
	public bool IsDead { get { return current == 0; } }

	public Health()
	{
		max = baseHealth;
	}

	public Health(int baseHealth)
	{
		this.baseHealth = baseHealth;
		max = baseHealth;
	}

	public void Reset()
	{
		current = max;
	}

	public void ApplyDamage(int amount)
	{
		current = Mathf.Clamp(current - amount, 0, max);
	}

	public void Heal(int amount)
	{
		ApplyDamage(-amount);
	}
}
