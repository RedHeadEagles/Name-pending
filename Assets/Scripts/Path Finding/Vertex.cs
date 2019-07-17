using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[System.Serializable]
public class Vertex : IEnumerable<Vector2Int>
{
	public Vector2Int location;

	public int X
	{
		get { return location.x; }
		set { location.x = value; }
	}

	public int Y
	{
		get { return location.y; }
		set { location.y = value; }
	}

	public Vector2 worldLocation;

	public List<Vector2Int> connections = new List<Vector2Int>();

	public int ActiveConnections { get { return connections.Count; } }

	public Vertex() { }

	public Vertex(int x, int y, Vector2 worldLocation)
	{
		location = new Vector2Int(x, y);
		this.worldLocation = worldLocation;
	}

	public void Link(Vertex end)
	{
		if (end == null || connections.Contains(end.location))
			return;

		connections.Add(end.location);
		end.connections.Add(location);
	}

	public void Unlink(Vertex end)
	{
		if (end == null)
			return;

		for (int i = 0; i < connections.Count; i++)
		{
			if (connections[i] == end.location)
			{
				connections.RemoveAt(i);
				break;
			}
		}

		for (int i = 0; i < end.connections.Count; i++)
		{
			if (end.connections[i] == location)
			{
				end.connections.RemoveAt(i);
				break;
			}
		}
	}

	public static float Distance(Vertex a, Vertex b)
	{
		return Vector2Int.Distance(a.location, b.location);
	}

	public IEnumerator<Vector2Int> GetEnumerator()
	{
		foreach (var neighbor in connections)
			yield return neighbor;
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		foreach (var neighbor in connections)
			yield return neighbor;
	}

	public static Vertex Create(int x, int y, Vector2 worldLocation)
	{
		return new Vertex(x, y, worldLocation);
	}
}
