using UnityEngine;

[CreateAssetMenu(menuName = "Game/Characters/Character")]
public class CharacterData : ScriptableObject
{
	public new string name;

	[Header("General Stats")]
	public float baseHealth = 100;

	public float baseEnergy = 100;

	[Range(0f, 1f)]
	public float baseEnergyRegen = 0.01f;

	public float baseSpeed = 1;

	public StatsData stats = new StatsData();

	[Range(0f, 1f)]
	[Tooltip("Percentage of movement speed to use when walking")]
	public float walkSpeed = 0.25f;

	[Header("Animations")]
	public AnimationClip walkAnimation;
}
