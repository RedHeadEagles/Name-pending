using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equipment/Weapon")]
[System.Serializable]
public class Weapon : Item
{
	public float lightDamage;

	public float lightStaminaUsage;

	public float heavyDamage;

	public float heavyStaminaUsage;
}
