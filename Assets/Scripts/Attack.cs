using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Attack : ScriptableObject
{
	public new string name;

	public float baseDamage = 10;

	public float coolDown = 1;
}
