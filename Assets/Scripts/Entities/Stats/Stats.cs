using UnityEngine;
using System.Collections;

[System.Serializable]
public class Stats
{
	public Health health = new Health(0.05f);

	public Stamina stamina = new Stamina(0.05f);

	public Speed speed = new Speed(0.05f);

	public Stat defense = new Stat(0.05f);

	public Stat attack = new Stat(0.05f);
}
