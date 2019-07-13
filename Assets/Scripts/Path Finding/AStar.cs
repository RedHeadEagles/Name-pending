using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AStar
{
	private static float EstiamteCost(Vertex start, Vertex end)
	{
		return Vector2Int.Distance(start.location, end.location);
	}

	private static List<Vector2> BuildPath(Mesh2D mesh, Dictionary<Vertex, Vertex> cameFrom, Vertex start, Vertex end)
	{
		var path = new List<Vector2>();

		while (end != start)
		{
			path.Insert(0, mesh.ToWorldGrid(end.location));
			end = cameFrom[end];
		}

		return path;
	}

	public static List<Vector2> FindPath(Mesh2D mesh, Vector2 startLocation, Vector2 endLocation)
	{
		var start = mesh.GetClosestVertex(startLocation);
		var end = mesh.GetClosestVertex(endLocation);

		if (start == null || end == null)
			return null;

		var open = new List<Vertex> { start };
		var closed = new List<Vertex>();
		var cameFrom = new Dictionary<Vertex, Vertex>();
		var gScore = new Dictionary<Vertex, float>();
		var fScore = new Dictionary<Vertex, float>();

		gScore[start] = 0;
		fScore[start] = EstiamteCost(start, end);

		while (open.Count > 0)
		{
			Vertex current = open[0];
			open.RemoveAt(0);

			if (current == end)
				return BuildPath(mesh, cameFrom, start, end);

			foreach (var neighbor in current.connections)
			{
				//var score = gScore[current] + EstiamteCost(current, neighbor);
			}
		}

		return null;
	}
}
