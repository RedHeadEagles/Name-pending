using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NavigationMesh))]
public class NavMesh2DEditor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		var mesh = target as NavigationMesh;

		if (GUILayout.Button("Rebuild Navigation Mesh"))
			mesh.BuildMesh();
	}
}