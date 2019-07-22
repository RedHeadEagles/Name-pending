using UnityEngine;

[System.Serializable]
public class Stamina : EntityResource
{
	[SerializeField]
	[Range(-1f, 1f)]
	private float regenPercentage = 0.1f;

	public float RegenPercentage { get { return regenPercentage; } }

	public float RegenRate { get { return regenPercentage * Max; } }

	public Stamina(float levelModifier) : base(levelModifier)
	{
		BaseValue = 100;
	}

	public void DoRegen(float deltaTime)
	{
		Current += RegenRate * deltaTime;
	}
}
