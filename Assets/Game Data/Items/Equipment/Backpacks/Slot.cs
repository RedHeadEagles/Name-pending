/// <summary>
/// Stores what is currently being stored in one of a backpack's many slots
/// </summary>
[System.Serializable]
public class Slot
{
	/// <summary>
	/// The item beings stored
	/// </summary>
	public ItemData item;

	/// <summary>
	/// Total number of items in this stack
	/// </summary>
	public uint quantity;

	/// <summary>
	/// Is this slot currently empty
	/// </summary>
	public bool IsEmpty { get { return quantity == 0; } }

	/// <summary>
	/// Does this slot currently have something in it
	/// </summary>
	public bool NotEmpty { get { return quantity > 0; } }


	public bool Add(uint quantity)
	{
		quantity = this.quantity + quantity;
		if(item.maxStack > quantity)
		{
			this.quantity = quantity;
			return true;
		}

		return false;
	}
}
