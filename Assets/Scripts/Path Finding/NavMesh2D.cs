using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class NavMesh2D : MonoBehaviour
{
	[SerializeField]
	[Tooltip("Draw the mesh in the scene for debuging")]
	private bool render = false;

	[SerializeField]
	private LayerMask terrainLayer;

	/// <summary>
	/// Distance between vertices
	/// </summary>
	public float size = 1;

	[SerializeField]
	private Vector2 origin;

	[SerializeField]
	public int width;

	[SerializeField]
	public int height;

	[SerializeField]
	private NavVertex[] vertices;

	public NavMesh2D() { }

	public NavVertex GetVertex(int x, int y)
	{
		if (x < 0 || x >= width || y < 0 || y >= height)
			return null;

		return vertices[x + width * y];
	}

	public void BuildMesh()
	{
		Debug.Log("Rebuilding NavMesh");

		Debug.Log("Calculating bounding box");

		Collider2D collider = GetComponent<Collider2D>();

		var min = collider.bounds.min;
		var max = collider.bounds.max;
		origin = min;

		Vector2 bounds = new Vector2(max.x - min.x, max.y - min.y);
		width = (int)(bounds.x / size) + 1;
		height = (int)(bounds.y / size) + 1;

		Debug.Log("Placing down all vertices");

		vertices = new NavVertex[width * height];

		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				var location = new Vector3(x, y, 0) * size;
				location += min;
				vertices[x + width * y] = new NavVertex(new Vector2Int(x, y), location);
			}
		}

		Debug.Log("Linking vertices");

		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				var vertex = GetVertex(x, y);

				if (vertex == null)
					continue;
				
				// Link the top vertex
				var end = GetVertex(x, y + 1);
				if (CanPathBetween(vertex, end, terrainLayer))
				{
					vertex.Top = end.location;
					end.Bottom = vertex.location;
				}

				// Link the top right vertex
				end = GetVertex(x + 1, y + 1);
				if (CanPathBetween(vertex, end, terrainLayer))
				{
					vertex.TopRight = end.location;
					end.BottomLeft = vertex.location;
				}

				// Link the right vertex
				end = GetVertex(x + 1, y);
				if (CanPathBetween(vertex, end, terrainLayer))
				{
					vertex.Right = end.location;
					end.Left = vertex.location;
				}

				// Link bottom right vertex
				end = GetVertex(x + 1, y - 1);
				if (CanPathBetween(vertex, end, terrainLayer))
				{
					vertex.BottomRight = end.location;
					end.TopLeft = vertex.location;
				}
			}
		}

		Debug.Log("Done!");
	}

	public Vector2 ToWorldCords(Vector2Int location)
	{
		return new Vector2(location.x, location.y) * size + origin;
	}

	public bool CanPathBetween(NavVertex a, NavVertex b, int layerMask)
	{
		if (a == null || b == null)
			return false;

		var start = a.worldLocation;
		var end = b.worldLocation;
		return Physics2D.Raycast(start, end - start, Vector2.Distance(start, end), layerMask).collider == null;
	}

	private void OnDrawGizmos()
	{
		if (!render || vertices == null)
			return;

		var color = Gizmos.color;
		Gizmos.color = Color.green;

		foreach (var vertex in vertices)
		{
			if (vertex == null)
				continue;

			Gizmos.DrawWireSphere(vertex.worldLocation, 0.1f);

			if (vertex.connections == null)
				continue;
			
			foreach (var link in vertex.connections)
			{
				if (link.x == int.MaxValue)
					continue;

				var end = GetVertex(link.x, link.y);
				if (end == null)
					return;

				Gizmos.DrawRay(vertex.worldLocation, end.worldLocation - vertex.worldLocation);
			}
		}

		Gizmos.color = color;
	}
}
