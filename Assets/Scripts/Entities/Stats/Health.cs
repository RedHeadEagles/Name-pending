using UnityEngine;

[System.Serializable]
public class Health : EntityResource
{
	/// <summary>
	/// Is this entity immune to all forms of damage
	/// </summary>
	[Tooltip("Is this entity immune to all forms of damage")]
	public bool invulnerable = false;

	public Health(float levelModifier) : base(levelModifier)
	{
		BaseValue = 100;
	}

	/// <summary>
	/// Deals damage to this entity, returns amount of overkill
	/// </summary>
	/// <param name="amount">Amount of damage to deal</param>
	/// <returns>Resturns how much damage was overkill</returns>
	public float Damage(float amount)
	{
		if (invulnerable)
			return 0;

		current -= amount;

		if(current<0)
		{
			amount = -current;
			current = 0;
		}

		return amount;
	}

	/// <summary>
	/// Heals this entity the given amount, returns amount of overhealing
	/// </summary>
	/// <param name="amount">Number of hit points to heal</param>
	/// <returns>Returns number of hit points over healed</returns>
	public float Heal(float amount)
	{
		current += amount;

		if(current>Max)
		{
			amount = Max - current;
			current = Max;
		}

		return amount;
	}
}
