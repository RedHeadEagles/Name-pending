using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Items/Item")]
public class Item : ScriptableObject
{
	public new string name;

	[TextArea(2, 10)]
	public string description;

	public int value;

	[Range(0, 60)]
	public int maxStack = 1;
	
	public Sprite icon;
}
