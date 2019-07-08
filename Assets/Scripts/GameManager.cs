using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
	[SerializeField]
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
}
