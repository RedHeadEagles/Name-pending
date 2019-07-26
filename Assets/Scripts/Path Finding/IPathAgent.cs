using UnityEngine;
using System.Collections.Generic;

public interface IPathAgent
{
	Transform transform { get; }

	void OnPathFound(List<Vector3> path, float pathTime);

	void OnPathFailed(float pathTime);
}
