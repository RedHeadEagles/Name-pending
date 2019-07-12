using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{
	public Mesh2D navMesh2D;

	private void Start()
	{

	}

	private void OnDrawGizmos()
	{
		//navMesh2D.DrawDebug();
	}
}
