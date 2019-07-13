using UnityEngine;
using System.Collections;

[CreateAssetMenu]
public class Gold : ScriptableObject
{
	public int min;

	public int max;

	public int Value
	{
		get { return Random.Range(min, max); }
	}
}
