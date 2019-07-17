using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	public Slot[] slots;

	public int FilledCount { get { return 0; } }

	public bool IsEmpty { get { return FilledCount == 0; } }

	public Slot GetSlot(int i)
	{
		if (i < 0 || i >= slots.Length)
			return null;

		return slots[i];
	}

	public bool CanInsert(ItemData item, int quantity = 1)
	{
		return false;
	}

	public void Insert(ItemData item, int quantity=1)
	{

	}

	public bool CanTransferTo(Inventory other)
	{
		return false;
	}
}
