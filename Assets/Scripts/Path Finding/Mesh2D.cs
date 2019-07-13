using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class Mesh2D : ScriptableObject
{
	[Range(float.Epsilon, 10f)]
	public float size = 1;

	[SerializeField]
	[HideInInspector]
	public int width = 0;

	[SerializeField]
	[HideInInspector]
	public int height = 0;

	[SerializeField]
	[Tooltip("The bottom left corner of the mesh")]
	public Vector2 min;

	[SerializeField]
	[Tooltip("The top right corner of the mesh")]
	public Vector2 max;

	[SerializeField]
	[Tooltip("Used to figure out what parts of the mesh are actually part the desired map")]
	public Vector2 origin;

	[SerializeField]
	public LayerMask layerMask;

	[SerializeField]
	[HideInInspector]
	public Vertex[] vertices;

	private int ToIndex(int x, int y)
	{
		return x + width * y;
	}

	private bool InBounds(int i)
	{
		return i >= 0 && i < vertices.Length;
	}

	public Vertex this[int x, int y]
	{
		get
		{
			int i = ToIndex(x, y);

			if (InBounds(i))
				return vertices[i];

			return null;
		}
		set
		{
			int i = ToIndex(x, y);

			if (InBounds(i))
				vertices[i] = value;
		}
	}

	public Vector2 ToWorldGrid(Vector2Int meshLocation)
	{
		return new Vector2(meshLocation.x, meshLocation.y) * size + min;
	}

	public Vertex GetClosestVertex(Vector2 location)
	{
		location -= min;
		location /= size;
		var close = new Vector2Int((int)location.x, (int)location.y);
		var vertex = this[close.x, close.y];
		return vertex;
	}

	public void BuildMesh()
	{
		Debug.Log("Rebuilding Mesh for: " + name);
		CreateVertices();
		LinkVertices();
		RemoveUselessVertices();
		RemoveBrokenLinks();
		RemoveUnpathable();
		RemoveBrokenLinks();
	}

	private void CreateVertices()
	{
		var bounds = (max - min) / size;
		width = (int)bounds.x + 1;
		height = (int)bounds.y + 1;

		vertices = new Vertex[width * height];

		for (int x = 0; x < width; x++)
			for (int y = 0; y < height; y++)
				this[x, y] = new Vertex(x, y);
	}

	private void LinkVertices()
	{
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				var vertex = this[x, y];

				LinkVertex(vertex, x + 1, y);
				LinkVertex(vertex, x, y + 1);
				LinkVertex(vertex, x + 1, y + 1);
				LinkVertex(vertex, x + 1, y - 1);
			}
		}
	}

	private void LinkVertex(Vertex vertex, int x, int y)
	{
		var end = this[x, y];
		if (Pathable(vertex, end))
			vertex.Link(end);
	}

	private bool Pathable(Vertex a, Vertex b)
	{
		if (a == null || b == null)
			return false;

		var start = ToWorldGrid(a.location);
		var end = ToWorldGrid(b.location);
		var dir = end - start;
		return Physics2D.Raycast(start, dir.normalized, dir.magnitude, layerMask).collider == null;
	}

	private void RemoveUselessVertices()
	{
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				var vertex = this[x, y];

				if (vertex.ActiveConnections < 3)
					this[x, y] = null;
			}
		}
	}

	private void RemoveBrokenLinks()
	{
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				var vertex = this[x, y];
				if (vertex == null)
					continue;

				for (int i = vertex.connections.Count - 1; i >= 0; i--)
				{
					var link = vertex.connections[i];
					var end = this[link.x, link.y];

					if (end == null)
						vertex.connections.RemoveAt(i);
				}
			}
		}
	}

	private void RemoveUnpathable()
	{
		var start = GetClosestVertex(origin);

	}

	public void DrawDebug()
	{
		var color = Gizmos.color;
		Gizmos.color = Color.green;

		foreach (var vertex in vertices)
		{
			if (vertex == null)
				continue;

			var start = ToWorldGrid(vertex.location);
			Gizmos.DrawWireSphere(start, 0.1f);

			foreach (var link in vertex.connections)
			{
				var end = ToWorldGrid(link);
				Gizmos.DrawRay(start, end - start);
			}
		}

		Gizmos.color = color;
	}
}
