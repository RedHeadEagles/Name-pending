using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SpriteAnimation : ScriptableObject
{
	/// <summary>
	/// How many frames per second are displayed
	/// </summary>
	[Tooltip("How many frames per second are displayed")]
	[Range(float.Epsilon, 144)]
	public float frameRate = 1;

	/// <summary>
	/// All the frames that make up this animation
	/// </summary>
	[Tooltip("All the frames that make up this animation")]
	public Sprite[] frames;
}
