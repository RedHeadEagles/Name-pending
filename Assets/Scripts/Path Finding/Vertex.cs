using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[System.Serializable]
public class Vertex : IEnumerable<Point>
{
	public bool disabled = false;

	public Point location;

	public Vector2 worldLocation;

	public List<Point> neighbors = new List<Point>();

	public Vertex(Vector2Int location, Vector2 worldLocation)
	{
		this.location = location;
		this.worldLocation = worldLocation;
	}

	public void Disable()
	{
		disabled = true;
		location = Vector2Int.zero;
		worldLocation = Vector2Int.zero;
		neighbors.Clear();
	}

	public IEnumerator<Point> GetEnumerator()
	{
		foreach (var neighbor in neighbors)
			yield return neighbor;
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		foreach (var neighbor in neighbors)
			yield return neighbor;
	}
}
