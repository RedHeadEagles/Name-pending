using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
	private Player player;

	/// <summary>
	/// Provides qick access to the player entity
	/// </summary>
	public static Player Player
	{
		get
		{
			if (Instance.player == null)
				Instance.player = FindObjectOfType<Player>();

			return Instance.player;
		}
	}

	private Terrain terrain = null;

	public static Terrain Terrain
	{
		get
		{
			if (Instance.terrain == null)
				Instance.terrain = FindObjectOfType<Terrain>();

			return Instance.terrain;
		}
	}
}
