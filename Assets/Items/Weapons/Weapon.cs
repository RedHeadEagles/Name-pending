using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Equipment/Weapon")]
public class Weapon : Equipment
{
	public Attack lightAttack;

	public Attack heavyAttack;

	public Attack uniqueAttack;
}
