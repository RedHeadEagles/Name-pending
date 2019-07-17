using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Game/Loot Table")]
public class LootTableData : ScriptableObject
{
	public int maxDrops = 5;

	public LootData[] items;
}
