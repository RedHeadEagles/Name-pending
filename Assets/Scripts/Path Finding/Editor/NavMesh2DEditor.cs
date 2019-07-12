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
		{
			mesh.BuildMesh();
		}
	}
}