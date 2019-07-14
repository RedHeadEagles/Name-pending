using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Character : ScriptableObject
{
	public new string name;

	[Header("General Stats")]
	public int health = 100;

	public int stamina = 100;

	[Range(0f, 1f)]
	public float staminaRegen = 0.01f;

	public uint attack = 1;

	public uint speed = 1;

	[Range(0f, 1f)]
	[Tooltip("Percentage of movement speed to use when walking")]
	public float walkSpeed = 0.25f;

	/// <summary>
	/// 10 damage to 0 defense = 10 damage
	/// 10 damage to 5 defense = 5 damage
	/// 10 damage to 9 defense = 1 damage
	/// 10 damage to 10+ defense = 1 damage
	/// 10 damage to -5 defense = 15 damage
	/// </summary>
	public int defense = 1;

	[Header("Current buffs and debuffs")]
	public List<StatusEffect> statusEffects = new List<StatusEffect>();
}
