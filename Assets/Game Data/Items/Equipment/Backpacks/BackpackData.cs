using UnityEngine;

[CreateAssetMenu(menuName ="Game/Equipment/Backpack")]
public class BackpackData : EquipmentData
{
	/// <summary>
	/// Total number of slots this backpack has
	/// </summary>
	[Range(1, 60)]
	public int slots = 1;
}
