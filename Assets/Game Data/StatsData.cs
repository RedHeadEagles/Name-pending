[System.Serializable]
public class StatsData
{
	/// <summary>
	/// Each point increases health by 5%
	/// </summary>
	public int health;

	/// <summary>
	/// Each point increases energy by 5%
	/// </summary>
	public int energy;

	/// <summary>
	/// 0.01% energy regeneration per point
	/// </summary>
	public int energyRegen;

	/// <summary>
	/// Each point in attack increases damage by 5%
	/// </summary>
	public int attack;

	/// <summary>
	/// 10 damage to 0 defense = 10 damage
	/// 10 damage to 5 defense = 5 damage
	/// 10 damage to 9 defense = 1 damage
	/// 10 damage to 10+ defense = 1 damage
	/// 10 damage to -5 defense = 15 damage
	/// </summary>
	public int defense;

	/// <summary>
	/// Each point in speed increases movement speed by 5%, passive dodge by 0.01% and energy regen by 0.1%
	/// </summary>
	public int speed;
}
