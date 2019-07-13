using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	public string spawned;

	[Range(0, 10)]
	public int minCount;

	[Range(1, 10)]
	public int maxCount = 1;

	public Collider2D area;

	void Start()
	{
		if (area == null)
			area = GetComponent<Collider2D>();

		if (area == null)
		{
			Debug.LogError(string.Format("Spawner '{0}' has no collider", gameObject.name));
			return;
		}

		area.isTrigger = true;

		int toSpawn = Random.Range(minCount, maxCount);

		Vector2 min = area.bounds.min;
		Vector2 max = area.bounds.max;

		while (toSpawn > 0)
		{
			Vector2 location;
			do
			{
				location.x = Random.Range(min.x, max.x);
				location.y = Random.Range(min.y, max.y);
			} while (!area.OverlapPoint(location));

			var spawn = ObjectPool.Spawn(spawned);
			spawn.transform.position = location;

			toSpawn--;
		}
	}
}
