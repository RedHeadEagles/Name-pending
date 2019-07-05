using System.Collections.Generic;
using UnityEngine;
using ObjectPoolInternal;

/// <summary>
/// Speeds up object creation by instead reusing objects rather than destroying them when finished
/// </summary>
[RequireComponent(typeof(PoolRegistar))]
public sealed class ObjectPool : MonoSingleton<ObjectPool>
{
	private static Dictionary<string, Pool> pools = new Dictionary<string, Pool>();

	public static Transform Container { get { return Instance.transform; } }

	/// <summary>
	/// Check if the pool already has a registar in it
	/// </summary>
	public static bool IsRegistered(string name)
	{
		return pools.ContainsKey(name);
	}

	/// <summary>
	/// Registers an object to the pool
	/// </summary>
	/// <param name="obj">The object to be used as the master</param>
	public static void Register(GameObject obj)
	{
		Register(obj.name, obj);
	}

	/// <summary>
	/// Registers an object to the pool
	/// </summary>
	/// <param name="name">The name to register the object under</param>
	/// <param name="obj">The object to be used as the master</param>
	public static void Register(string name, GameObject obj)
	{
		obj.name = name;

		if (IsRegistered(name))
		{
			Debug.LogWarning("ObjectPool::Attempted to register an object that is already registered: " + obj.name);
			return;
		}

		if (name == null || name == "")
		{
			Debug.LogError("ObjectPool::Cannot register an object with an empty for null name");
			return;
		}

		if (obj == null)
		{
			Debug.LogError("ObjectPool::Cannot register a null object");
			return;
		}

		pools[name] = new Pool(obj);
	}

	/// <summary>
	/// Deregisters an object from this pool
	/// </summary>
	/// <param name="name"></param>
	public static void Deregister(string name)
	{
		if (IsRegistered(name))
			pools[name].Clear();
		else
			Debug.LogError("ObjectPool::Cannot deregister a non extant pool");
	}

	public static GameObject Spawn(string name, IPoolSpawner spawner = null)
	{
		var pool = pools[name];

		GameObject spawn = pool.Rent();

		if (spawner != null)
			spawner.Spawn(spawn);

		return spawn;
	}

	public static GameObject[] SpawnMultiple(string name, int quantity, IPoolSpawner spawner = null)
	{
		GameObject[] spawns = new GameObject[quantity];

		for (int i = 0; i < quantity; i++)
			spawns[i] = Spawn(name, spawner);

		return spawns;
	}

	public static void Despawn(GameObject obj)
	{
		if (IsRegistered(obj.name))
			pools[obj.name].Return(obj);
		else
			Destroy(obj);
	}

	/// <summary>
	/// Destroys all currently inactive copies in all pools
	/// </summary>
	public static void Clean()
	{
		foreach (var pool in pools.Values)
			pool.Clean();
	}

	public static void Clear()
	{
		foreach (var pool in pools.Values)
			pool.Clear();

		pools.Clear();
	}
}
