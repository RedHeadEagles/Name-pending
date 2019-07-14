using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class Backpack : Item
{
	public Slot[] slots;

	public int ItemCount
	{
		get
		{
			int count = 0;

			foreach (var slot in slots)
				if (slot.NotEmpty)
					count++;

			return count;
		}
	}

	public bool IsEmpty { get { return ItemCount == 0; } }

	public bool NotEmpty { get { return ItemCount > 0; } }

	public bool TransferTo(Backpack backpack)
	{
		if (ItemCount < slots.LongLength)
			return false; // New backpack can't hold all the items in the current backpack

		return true;
	}

	public bool Add(Item item, uint quantity = 1)
	{
		return false;
	}
}
