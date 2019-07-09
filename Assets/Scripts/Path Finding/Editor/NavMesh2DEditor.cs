using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NavMesh2D))]
public class NavMesh2DEditor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		if(GUILayout.Button("Rebuild NavMesh"))
		{
			var mesh = target as NavMesh2D;
			mesh.BuildMesh();
		}
	}
}
