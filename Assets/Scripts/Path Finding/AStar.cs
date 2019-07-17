using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AStar
{
	private class Heap
	{
		private List<Node> heap = new List<Node>();

		public bool IsEmpty { get { return heap.Count == 0; } }

		public void Insert(Node data)
		{
			heap.Insert(0, data);
		}

		public Node Pop()
		{
			if (IsEmpty)
				return default;

			heap = heap.OrderBy(i => i.fScore).ToList();
			Node d = heap[0];
			heap.RemoveAt(0);
			return d;
		}
	}

	private class Node
	{
		public Vector2Int location;

		public float gScore = float.MaxValue;

		public float fScore = float.MaxValue;

		public Node cameFrom;

		public Node(Vector2Int location)
		{
			this.location = location;
		}
	}

	private static float Cost(Vector2Int a, Vector2Int b)
	{
		return Vector2Int.Distance(a, b);
	}

	public static List<Vector2> FindPath(Mesh2D mesh, Vector2Int start, Vector2Int end)
	{
		var map = new Node[mesh.width, mesh.height];
		var open = new Heap();

		for (int x = 0; x < mesh.width; x++)
			for (int y = 0; y < mesh.height; y++)
				map[x, y] = new Node(new Vector2Int(x, y));

		map[start.x, start.y].gScore = 0;
		map[start.x, start.y].fScore = Cost(start, end);

		while(!open.IsEmpty)
		{
			var current = open.Pop();
			if (current.location == end)
				return null;

			var vertex = mesh[current.location.x, current.location.y];
			foreach (var neighbor in vertex)
			{

			}
		}

		return null;
	}
}
