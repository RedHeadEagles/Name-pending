using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class Item : ScriptableObject
{
	public new string name;

	[TextArea(2, 10)]
	public string description;

	public uint value;

	[Range(0, 60)]
	public uint maxStack = 1;
	
	public Sprite icon;
}
