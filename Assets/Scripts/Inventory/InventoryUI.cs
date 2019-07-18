using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
	public GameObject slotPrefab;

	[SerializeField]
	private Inventory inventory;

	public Inventory Inventory
	{
		set
		{
			inventory = value;
			OnWatchedInventoryChanged();
		}
	}

	private SlotUI[] slots;

	private void Start()
	{
		OnWatchedInventoryChanged();
	}

	private void OnWatchedInventoryChanged()
	{
		foreach (Transform slot in transform)
			Destroy(slot.gameObject);

		slots = new SlotUI[inventory.Size];

		for (int i = 0; i < inventory.Size; i++)
		{
			var slotObj = Instantiate(slotPrefab, transform);
			var slot = slotObj.GetComponent<SlotUI>();
			slots[i] = slot;

			if (inventory[i].item != null)
				slot.icon.sprite = inventory[i].item.icon;
			slot.quantity.text = inventory[i].quantity.ToString();
		}
	}
}
