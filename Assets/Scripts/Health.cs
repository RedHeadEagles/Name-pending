using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Health : Stat
{
	/// <summary>
	/// Returns true if this entity has zero health left
	/// </summary>
	public bool IsDead { get { return Current == 0; } }

	public Health(int baseHealth) : base(baseHealth) { }

	public void ApplyDamage(int amount)
	{
		Current = Mathf.Clamp(Current - amount, 0, Max);
	}

	public void Heal(int amount)
	{
		ApplyDamage(-amount);
	}
}
