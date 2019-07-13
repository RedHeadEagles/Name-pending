using UnityEngine;
using System.Collections;

public class EntityObject : ScriptableObject
{
	public new string name;

	public float health = 100;

	public float healthRegen = 1;

	public float stamina = 100;

	public float staminaRegen = 1;

	public float speed = 10;
}
