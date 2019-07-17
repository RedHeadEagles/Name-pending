using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoSingleton<Terrain>
{
	public Mesh2D navMesh2D;

	public static List<Vector2> FindPath(Vector2 start, Vector2 end)
	{
		Debug.Log(string.Format("Pathfinding from {0} to {1}", start, end));
		var mesh = Instance.navMesh2D;
		var s = mesh.GetClosestVertex(start).location;
		var e = mesh.GetClosestVertex(end).location;
		return AStar.FindPath(Instance.navMesh2D, s, e);
	}

	private void Start()
	{

	}


	private void OnDrawGizmosSelected()
	{
		var color = Gizmos.color;
		Gizmos.color = Color.green;

		foreach (var vertex in navMesh2D)
		{
			if (vertex == null)
				continue;

			var start = navMesh2D.ToWorldGrid(vertex.location);
			Gizmos.DrawWireSphere(start, 0.1f);

			foreach (var link in vertex.connections)
			{
				var end = navMesh2D.ToWorldGrid(link.x, link.y);
				Gizmos.DrawRay(start, end - start);
			}
		}

		Gizmos.color = color;
	}
}
