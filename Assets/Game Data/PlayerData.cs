using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName ="Game/Characters/Player")]
public class PlayerData : CharacterData
{
	[Header("Starting Gear")]
	public Armor startArmor;

	public Weapon startWeapon;

	public Trinket startTrinket;

	public Special startSpecial;

	public BackpackData startBackpack;
}
