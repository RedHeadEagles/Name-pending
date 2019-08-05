using UnityEngine;

[CreateAssetMenu]
public class Character : ScriptableObject
{
	public int health = 100;

	public int mana = 100;

	public int initiative = 10;

	public SpriteSheet worldSprites;

	public SpriteSheet battleSprits;
}
