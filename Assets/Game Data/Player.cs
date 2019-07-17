using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class Player : CharacterData
{
	[Header("Starting Gear")]
	public Armor startArmor;

	public Weapon startWeapon;

	public Trinket startTrinket;

	public Special startSpecial;

	public BackpackData startBackpack;
}
