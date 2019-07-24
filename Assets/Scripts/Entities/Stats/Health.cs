using UnityEngine;

[System.Serializable]
public class Health : EntityResource
{
	/// <summary>
	/// Is this entity immune to all forms of damage
	/// </summary>
	public bool invulnerable = false;

	public Health(float levelModifier) : base(levelModifier)
	{
		BaseValue = 100;
	}

	public void Damage(float amount)
	{
		if (invulnerable)
			return;

		Current -= amount;
	}

	public void Heal(float amount)
	{
		Current += amount;
	}
}
