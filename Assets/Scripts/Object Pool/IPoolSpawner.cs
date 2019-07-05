using UnityEngine;

/// <summary>
/// Used to control behavior when spawning objects from the object pool
/// </summary>
public interface IPoolSpawner
{
	void Spawn(GameObject gameObject);
}