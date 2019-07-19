using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Game/Effects/Effect")]
public class EffectData : ScriptableObject
{
	public new string name;

	[TextArea(1, 5)]
	public string description;

	public Sprite icon;

	public float duration;

	public StatsData statChanges;
}
