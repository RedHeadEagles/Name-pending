using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Vertex
{
	public Vector2Int location;

	public List<Vector2Int> connections = new List<Vector2Int>();

	public int ActiveConnections { get { return connections.Count; } }

	public Vertex() { }

	public Vertex(int x, int y)
	{
		location = new Vector2Int(x, y);
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

	public float DistanceTo(Vertex vertex)
	{
		return Vector2Int.Distance(location, vertex.location);
	}
}
