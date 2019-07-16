using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class Player : Character
{
	[Header("Starting Gear")]
	public Armor startArmor;

	public Weapon startWeapon;

	public Trinket startTrinket;

	public Special startSpecial;

	public Backpack startBackpack;
}
