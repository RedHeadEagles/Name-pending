using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
	public Vector2 playerSpawnLocation;

	private static Zone current;

	public static Zone Current
	{
		get
		{
			if (current == null)
				current = FindObjectOfType<Zone>();

			return current;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		Debug.Log(collision);
		var entity = collision.GetComponent<Entity>();
		if (entity == null)
			return;

		entity.ApplyDamage(float.MaxValue);
	}
}
