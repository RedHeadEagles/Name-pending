using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour, IEnumerable<Slot>
{
	public Slot[] slots;

	public int FilledCount { get { return 0; } }

	public bool IsEmpty { get { return FilledCount == 0; } }

	public int Size { get { return slots.Length; } }

	public Slot this[int i]
	{
		get { return slots[i]; }
		set { slots[i] = value; }
	}


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

	public IEnumerator<Slot> GetEnumerator()
	{
		foreach (var slot in slots)
			yield return slot;
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		foreach (var slot in slots)
			yield return slot;
	}
}
