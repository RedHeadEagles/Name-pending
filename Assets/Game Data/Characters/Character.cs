using UnityEngine;

[CreateAssetMenu(menuName = "Game/Characters/Character")]
public class CharacterData : ScriptableObject
{
	public new string name;

	[Header("General Stats")]
	public Stats stats = new Stats();

	[Range(0f, 1f)]
	[Tooltip("Percentage of movement speed to use when walking")]
	public float walkSpeed = 0.25f;

	[Header("Animations")]
	public AnimationClip walkAnimation;
}
