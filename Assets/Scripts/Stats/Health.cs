using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Health : Stat
{
	[SerializeField]
	private float regen = 1;

	public float Regen
	{
		get { return regen; }
	}

	/// <summary>
	/// Returns true if this entity has zero health left
	/// </summary>
	public bool IsDead { get { return Current == 0; } }

	/// <summary>
	/// Returns true if this entity has a non-zero amount of health
	/// </summary>
	public bool IsNotDead { get { return Current > 0; } }

	public Health(int baseHealth) : base(baseHealth) { }

	public void ApplyDamage(float amount)
	{
		Current -= amount;
	}

	public void Heal(float amount)
	{
		ApplyDamage(-amount);
	}

	public void DoRegen(float deltaTime)
	{
		Current += Regen * deltaTime;
	}
}
