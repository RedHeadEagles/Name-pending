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
	[HideInInspector]
	private int width;

	[SerializeField]
	[HideInInspector]
	private int height;

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
				vertices[x + width * y] = new NavVertex(location);
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
				if (vertex.CanPathTo(end, terrainLayer))
				{
					vertex.Top = end.location;
					end.Bottom = vertex.location;
				}

				// Link the top right vertex
				end = GetVertex(x + 1, y + 1);
				if (vertex.CanPathTo(end, terrainLayer))
				{
					vertex.TopRight = end.location;
					end.BottomLeft = vertex.location;
				}

				// Link the right vertex
				end = GetVertex(x + 1, y);
				if(vertex.CanPathTo(end,terrainLayer))
				{
					vertex.Right = end.location;
					end.Left = vertex.location;
				}

				// Link bottom right vertex
				end = GetVertex(x + 1, y - 1);
				if(vertex.CanPathTo(end, terrainLayer))
				{
					vertex.BottomRight = end.location;
					end.TopLeft = vertex.location;
				}
			}
		}

		Debug.Log("Done!");
	}

	private void OnDrawGizmos()
	{
		if (!render || vertices == null)
			return;

		var color = Gizmos.color;

		foreach (var vertex in vertices)
		{
			Gizmos.color = Color.green;
			vertex.DrawGizmos();
		}

		Gizmos.color = color;
	}
}
