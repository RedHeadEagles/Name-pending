using UnityEngine;

[CreateAssetMenu(menuName ="Game/Loot Table")]
public class LootTableData : ScriptableObject
{
	[System.Serializable]
	public struct Loot
	{
		public ItemData item;

		[Range(0f, 1f)]
		public float chance;
	}

	public int minGold;

	public int maxGold;

	public int maxDrops = 5;

	public Loot[] items;
}
