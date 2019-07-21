using UnityEngine;
using System.Collections;

[System.Serializable]
public class Stats
{
	public Health health = new Health(0.05f);

	public Stamina stamina = new Stamina(0.05f);

	public Stat speed = new Stat(0.05f);

	[Range(0f, 1f)]
	public float walkSpeedModifier = 0.25f;

	public float WalkSpeed { get { return speed.Value * walkSpeedModifier; } }

	public Stat defense = new Stat(0.05f);

	public Stat attack = new Stat(0.05f);
}
