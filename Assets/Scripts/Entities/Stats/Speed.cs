using UnityEngine;
using System.Collections;

[System.Serializable]
public class Speed : Stat
{
	[Range(0f, 1f)]
	public float walkModifier = 0.25f;

	public float Walk { get { return Value * walkModifier; } }

	public float dashModifier = 10;

	public float Dash { get { return Value * dashModifier; } }

	public float dashEnergyCost = 20;

	public Speed(float levelModifier) : base(levelModifier)
	{

	}
}
