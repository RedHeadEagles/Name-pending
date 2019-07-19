using UnityEngine;

[CreateAssetMenu(menuName = "Game/Effects/Buff")]
public class BuffData : EffectData
{
	/// <summary>
	/// Is the player allowed to cancel this effect
	/// </summary>
	public bool removeable = true;
}
