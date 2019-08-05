using UnityEngine;

[CreateAssetMenu]
public class SpriteSheet : ScriptableObject
{
	/// <summary>
	/// Sprite to use when no animation is playing
	/// </summary>
	[Tooltip("Sprite to use when no animation is playing")]
	public Sprite fallBack;

	/// <summary>
	/// All the animations this sprite has
	/// </summary>
	[Tooltip("All the animations this sprite has")]
	public SpriteAnimation[] animations;

	public SpriteAnimation this[string name]
	{
		get
		{
			foreach (var animation in animations)
				if (animation.name == name)
					return animation;

			return null;
		}
	}
}
