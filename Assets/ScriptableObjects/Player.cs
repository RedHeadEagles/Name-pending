using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Equipment
{
	public Armor armor;

	public Weapon weapon;

	public Trinket trinket;

	public Special special;

	public Backpack backpack;
}

[CreateAssetMenu]
public class Player : Character
{
	[Header("Starting Gear")]
	public Equipment startingGear = new Equipment();
}
