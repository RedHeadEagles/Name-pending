using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Equipment/Weapon")]
public class Weapon : EquipmentData
{
	public Attack lightAttack;

	public Attack heavyAttack;

	public Attack uniqueAttack;
}
