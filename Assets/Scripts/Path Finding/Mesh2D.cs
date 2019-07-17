using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class Mesh2D : ScriptableObject, IEnumerable<Vertex>
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

	public Vector2 ToWorldGrid(Vector2Int vector)
	{
		return ToWorldGrid(vector.x, vector.y);
	}

	public Vector2 ToWorldGrid(int x, int y)
	{
		return new Vector2(x, y) * size + min;
	}

	public Vertex GetClosestVertex(Vector2 location)
	{
		location -= min;
		location /= size;
		var close = new Vector2Int((int)location.x, (int)location.y);
		var vertex = this[close.x, close.y];
		return vertex;
	}

	public IEnumerator<Vertex> GetEnumerator()
	{
		foreach (var vertex in vertices)
			yield return vertex;
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		foreach (var vertex in vertices)
			yield return vertex;
	}
}
