using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Mesh2D))]
public class NavMesh2DEditor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		var mesh = target as Mesh2D;

		GUILayout.Space(32);
		GUILayout.Label(string.Format("Total vertices: {0}", mesh.vertices.Length));

		if (GUILayout.Button("Rebuild Navigation Mesh"))
			BuildMesh(mesh);
	}
	
	private void BuildMesh(Mesh2D mesh)
	{
		Debug.Log("Rebuilding Mesh for: " + target.name);
		CreateVertices(mesh);
		LinkVertices(mesh);
		RemoveUselessVertices(mesh);
		RemoveBrokenLinks(mesh);
	}

	private void CreateVertices(Mesh2D mesh)
	{
		var bounds = (mesh.max - mesh.min) / mesh.size;
		mesh.width = (int)bounds.x + 1;
		mesh.height = (int)bounds.y + 1;

		mesh.vertices = new Vertex[mesh.width * mesh.height];

		for (int x = 0; x < mesh.width; x++)
			for (int y = 0; y < mesh.height; y++)
				mesh[x, y] = Vertex.Create(x, y, new Vector2(x, y) * mesh.size + mesh.min);
	}

	private void LinkVertices(Mesh2D mesh)
	{
		for (int x = 0; x < mesh.width; x++)
		{
			for (int y = 0; y < mesh.height; y++)
			{
				var vertex = mesh[x, y];

				LinkVertex(mesh, vertex, x + 1, y);
				LinkVertex(mesh, vertex, x, y + 1);
				LinkVertex(mesh, vertex, x + 1, y + 1);
				LinkVertex(mesh, vertex, x + 1, y - 1);
			}
		}
	}

	private void LinkVertex(Mesh2D mesh, Vertex vertex, int x, int y)
	{
		var end = mesh[x, y];
		if (Pathable(mesh, vertex, end))
			vertex.Link(end);
	}

	private bool Pathable(Mesh2D mesh, Vertex a, Vertex b)
	{
		if (a == null || b == null)
			return false;

		var start = mesh.ToWorldGrid(a.location);
		var end = mesh.ToWorldGrid(b.location);
		var dir = end - start;
		return Physics2D.Raycast(start, dir.normalized, dir.magnitude, mesh.layerMask).collider == null;
	}

	private void RemoveUselessVertices(Mesh2D mesh)
	{
		for (int x = 0; x < mesh.width; x++)
		{
			for (int y = 0; y < mesh.height; y++)
			{
				var vertex = mesh[x, y];

				if (vertex.ActiveConnections < 3)
					mesh[x, y] = null;
			}
		}
	}

	private void RemoveBrokenLinks(Mesh2D mesh)
	{
		for (int x = 0; x < mesh.width; x++)
		{
			for (int y = 0; y < mesh.height; y++)
			{
				var vertex = mesh[x, y];
				if (vertex == null)
					continue;

				for (int i = vertex.connections.Count - 1; i >= 0; i--)
				{
					var link = vertex.connections[i];
					var end = mesh[link.x, link.y];

					if (end == null)
						vertex.connections.RemoveAt(i);
				}
			}
		}
	}
}