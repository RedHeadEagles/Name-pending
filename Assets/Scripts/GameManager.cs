using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
	public Player player;
	
	/// <summary>
	/// Provides qick access to the player entity
	/// </summary>
	public static Player Player { get { return Instance.player; } }
}
