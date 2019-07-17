using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Equipment/Weapon")]
public class Weapon : EquipmentData
{
	public AttackData lightAttack;

	public AttackData heavyAttack;

	public AttackData uniqueAttack;
}
