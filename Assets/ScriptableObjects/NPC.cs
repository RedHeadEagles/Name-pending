using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class NPC : Character
{
	[Range(0, 1f)]
	[Tooltip("Percentage of movement speed to use when idle/wandering")]
	public float idleSpeed = 0.25f;

	public float damageModifier = 1;

	public AggroSettings aggro;

	public Attack[] attacks;

	public LootTable lootTable;
}

[System.Serializable]
public class AggroSettings
{
	public bool hostileToPlayer = true;

	[Tooltip("How close must an entity get before this one will attack")]
	public float range = 10;

	[Tooltip("If the distance between the target and this entity is greater than this value flee home")]
	public float deaggro = 13;

	[Tooltip("If too far away from spawn location flee back to spawn")]
	public float flee = 20;
}