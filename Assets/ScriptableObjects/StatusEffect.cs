using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect : ScriptableObject
{
	public new string name;

	[TextArea(1, 5)]
	public string description;

	public Sprite icon;

	public float duration;

	public Stats stats = new Stats();
}
