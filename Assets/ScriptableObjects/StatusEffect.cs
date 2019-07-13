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

	[Range(0f, 1f)]
	public float applyChance = 1;
}
