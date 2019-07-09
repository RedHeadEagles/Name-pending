using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class NavVertex
{
	public Vector2Int location;

	public Vector2 worldLocation;
	
	public Vector2Int[] connections = new Vector2Int[8];

	public int ActiveConnections
	{
		get
		{
			if (connections == null)
				return 0;

			int count = 0;

			foreach (var link in connections)
				count += link.x < int.MaxValue ? 1 : 0;

			return count;
		}
	}

	public Vector2Int Left
	{
		get { return connections[0]; }
		set { connections[0] = value; }
	}

	public Vector2Int Right
	{
		get { return connections[1]; }
		set { connections[1] = value; }
	}

	public Vector2Int Top
	{
		get { return connections[2]; }
		set { connections[2] = value; }
	}

	public Vector2Int Bottom
	{
		get { return connections[3]; }
		set { connections[3] = value; }
	}

	public Vector2Int TopLeft
	{
		get { return connections[4]; }
		set { connections[4] = value; }
	}

	public Vector2Int TopRight
	{
		get { return connections[5]; }
		set { connections[5] = value; }
	}

	public Vector2Int BottomLeft
	{
		get { return connections[6]; }
		set { connections[6] = value; }
	}

	public Vector2Int BottomRight
	{
		get { return connections[7]; }
		set { connections[7] = value; }
	}

	public NavVertex() { }

	public NavVertex(Vector2Int location, Vector2 worldLocation)
	{
		this.location = location;
		this.worldLocation = worldLocation;

		for (int i = 0; i < connections.Length; i++)
			connections[i] = new Vector2Int(int.MaxValue, 0);
	}

	public float DistanceTo(NavVertex vertex)
	{
		return Vector2Int.Distance(location, vertex.location);
	}

	public float DistanceTo(Vector2Int location)
	{
		return Vector2Int.Distance(this.location, location);
	}
}
