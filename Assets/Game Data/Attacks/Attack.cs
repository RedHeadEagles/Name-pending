using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AttackData : ScriptableObject
{
	public new string name;

	public float baseDamage = 10;

	public float range = 1;

	public float energyCost = 20;

	public EffectData[] statusEffects;
}
