using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class NavVertex
{
	public Vector2 location;
	
	public Vector2[] connections = new Vector2[8];

	public int ActiveConnections
	{
		get
		{
			int count = 0;

			foreach (var link in connections)
				count += link == Vector2.positiveInfinity ? 0 : 1;

			return count;
		}
	}

	public Vector2 Left
	{
		get { return connections[0]; }
		set { connections[0] = value; }
	}

	public Vector2 Right
	{
		get { return connections[1]; }
		set { connections[1] = value; }
	}

	public Vector2 Top
	{
		get { return connections[2]; }
		set { connections[2] = value; }
	}

	public Vector2 Bottom
	{
		get { return connections[3]; }
		set { connections[3] = value; }
	}

	public Vector2 TopLeft
	{
		get { return connections[4]; }
		set { connections[4] = value; }
	}

	public Vector2 TopRight
	{
		get { return connections[5]; }
		set { connections[5] = value; }
	}

	public Vector2 BottomLeft
	{
		get { return connections[6]; }
		set { connections[6] = value; }
	}

	public Vector2 BottomRight
	{
		get { return connections[7]; }
		set { connections[7] = value; }
	}

	public NavVertex() { }

	public NavVertex(Vector2 location)
	{
		this.location = location;

		for (int i = 0; i < connections.Length; i++)
			connections[i] = Vector2.positiveInfinity;
	}

	public float DistanceTo(NavVertex vertex)
	{
		return Vector2.Distance(location, vertex.location);
	}

	public float DistanceTo(Vector2 location)
	{
		return Vector2.Distance(this.location, location);
	}

	public bool CanPathTo(NavVertex end, int layerMask)
	{
		if (end == null)
			return false;

		return Physics2D.Raycast(location, end.location - location, DistanceTo(end), layerMask).collider == null;
	}

	public bool CanPathBetween(NavVertex a, NavVertex b, int layerMask)
	{
		return a.CanPathTo(b, layerMask);
	}

	public void DrawGizmos()
	{
		Gizmos.DrawWireSphere(location, 0.1f);

		foreach (var link in connections)
		{
			if (link == null || link == Vector2.positiveInfinity)
				continue;

			Gizmos.DrawRay(location, link - location);
		}
	}
}
