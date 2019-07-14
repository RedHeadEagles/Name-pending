using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Equipment
{
	public Armor armor;

	public Weapon weapon;

	public Trinket trinket;

	public Special special;
}

[CreateAssetMenu]
[System.Serializable]
public class Player : Character
{
	[Header("Equiped Gear")]

	public Backpack backpack;

	public Equipment equipment = new Equipment();
}
