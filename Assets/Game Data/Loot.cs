using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct LootData
{
	public ItemData item;

	[Range(0f, 1f)]
	public float chance;
}
