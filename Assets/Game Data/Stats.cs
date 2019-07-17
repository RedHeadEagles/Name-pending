using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats
{
	public int health;

	public int energy;

	/// <summary>
	/// 0.01% energy regeneration per point
	/// </summary>
	public int energyRegen;

	public int attack;

	/// <summary>
	/// 10 damage to 0 defense = 10 damage
	/// 10 damage to 5 defense = 5 damage
	/// 10 damage to 9 defense = 1 damage
	/// 10 damage to 10+ defense = 1 damage
	/// 10 damage to -5 defense = 15 damage
	/// </summary>
	public int defense;

	public int speed;
}
