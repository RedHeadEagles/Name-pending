using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
	public new string name;

	[TextArea(2, 10)]
	public string description;

	public int value;

	public Sprite icon;
}
