using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class Player : EntityObject
{
	public Item[] inventory = new Item[12];

	public Armor armor;

	public Weapon weapon;

	public Trinket trinket;

	public Special special;
}
