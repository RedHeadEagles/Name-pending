using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class NavigationMesh : MonoBehaviour
{
	public Vertex[] vertices = new Vertex[0];

	public Vector3 Min { get { return Collider.bounds.min; } }

	public Vector3 Max { get { return Collider.bounds.max; } }

	public Vector3 Size { get { return Max - Min; } }

	public int width;

	public int height;

	[Range(float.Epsilon, 20f)]
	[Tooltip("Smaller values improves map accuracy but will increase CPU load")]
	public float vertexDistance = 2;

	[Range(0, 4)]
	public int minNeighbors = 3;

	public LayerMask terrainLayer;

	public BoxCollider2D Collider { get { return GetComponent<BoxCollider2D>(); } }

	public List<Vector3> FindPath(Vector3 start, Vector3 end)
	{
		return null;
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
		private set
		{
			int i = ToIndex(x, y);
			if (InBounds(i))
				vertices[i] = value;
		}
	}

	public Vertex this[Vector3 vector]
	{
		get
		{
			vector = vector - Min;
			vector /= vertexDistance;
			var loc = new Vector2Int((int)vector.x, (int)vector.y);

			return this[loc.x, loc.y];
		}
	}

	private int ToIndex(int x, int y)
	{
		return x + y * width;
	}

	private bool InBounds(int i)
	{
		return i >= 0 && i < vertices.Length;
	}

	private Vector3 ToWorld(Vector2Int location)
	{
		return new Vector3(location.x, location.y) * vertexDistance + Min;
	}

	public bool CanPath(Vector3 start, Vector3 end)
	{
		var dir = end - start;
		return Physics2D.Raycast(start, dir, dir.magnitude, terrainLayer).collider == null;
	}

	private void Link(Vertex a, Vertex b)
	{
		if (a == null || b == null) return;

		a.neighbors.Add(b.location);
		b.neighbors.Add(a.location);
	}

	private void Unlink(Vertex a, Vertex b)
	{
		if (a == null || b == null) return;


	}

	private void AttemptLink(Vertex a, Vertex b)
	{
		if (a == null || b == null) return;

		if (CanPath(a.worldLocation, b.worldLocation))
			Link(a, b);
	}

	public void BuildMesh()
	{
		Debug.Log("Building navigation mesh");

		// Calculated the required size of the map
		var size = Size / vertexDistance;
		width = (int)size.x + 1;
		height = (int)size.y + 1;

		// Create the map
		vertices = new Vertex[width * height];

		// Create all the vertices for the map
		for (int x = 0; x < width; x++)
			for (int y = 0; y < height; y++)
			{
				Vector2Int l = new Vector2Int(x, y);
				this[x, y] = new Vertex(l, ToWorld(l));
			}

		Debug.Log("Linking vertices");
		// Link all neighbors that have a possible path
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				var start = this[x, y];

				AttemptLink(start, this[x + 1, y]);
				AttemptLink(start, this[x, y + 1]);
				AttemptLink(start, this[x + 1, y + 1]);
				AttemptLink(start, this[x + 1, y - 1]);
			}
		}

		while (CullVertices()) ;

		Debug.Log("Finished building navigation mesh");
	}

	/// <summary>
	/// Remove any vertex that does not have enough neighbors
	/// </summary>
	private bool CullVertices()
	{
		Debug.Log("Culling verticies");

		bool removed = false;

		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				var v = this[x, y];

				if (!v.disabled && v.neighbors.Count < minNeighbors)
				{
					removed = true;
					this[x, y].Disable();
				}
			}
		}

		RemoveBrokenLinks();

		return removed;
	}

	/// <summary>
	/// Remove any connections that are now broken
	/// </summary>
	private void RemoveBrokenLinks()
	{
		Debug.Log("Removing broken links");

		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				var v = this[x, y];

				// Check that the current vertex is not disabled
				if (v == null || v.disabled)
					continue;

				for (int i = v.neighbors.Count - 1; i >= 0; i--)
				{
					// Get the neighbor
					var neighbor = v.neighbors[i];
					var n = this[neighbor.x, neighbor.y];

					// Check if the neighbor is disabled and remove if needed
					if (n == null || n.disabled)
						v.neighbors.RemoveAt(i);
				}
			}
		}
	}

	private void OnDrawGizmosSelected()
	{
		var color = Gizmos.color;
		Gizmos.color = Color.green;

		foreach (var vertex in vertices)
		{
			if (vertex == null || vertex.disabled)
				continue;

			Gizmos.DrawWireSphere(vertex.worldLocation, 0.2f);

			foreach (var neighbor in vertex)
			{
				var end = this[neighbor.x, neighbor.y];

				if (end == null || end.disabled)
					continue;

				Gizmos.DrawLine(vertex.worldLocation, end.worldLocation);
			}
		}

		Gizmos.color = color;
	}
}
