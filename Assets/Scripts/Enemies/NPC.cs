using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class NPC : ScriptableObject
{
	public new string name;

	public float health = 100;

	public float speed = 10;

	[Range(0, 1f)]
	[Tooltip("Percentage of movement speed to use when idle/wandering")]
	public float idleSpeed = 0.25f;
	
	public float damageModifier = 1;

	public Attack[] attacks;
}
