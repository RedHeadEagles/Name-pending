using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stores information about an item
/// </summary>
[CreateAssetMenu(menuName ="Game/Item")]
public class ItemData : ScriptableObject
{
	/// <summary>
	/// The name of the item
	/// </summary>
	public new string name;

	/// <summary>
	/// Extra flavor information for the player
	/// </summary>
	[TextArea(3, 10)]
	public string description;

	/// <summary>
	/// The item's sell value
	/// </summary>
	public int value;

	/// <summary>
	/// The maximum number of items this can stack up to
	/// </summary>
	[Range(0, 60)]
	public int maxStack = 1;
	
	/// <summary>
	/// Icon to use in the inventory screen
	/// </summary>
	public Sprite icon;
}
