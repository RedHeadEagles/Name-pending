using System.Collections.Generic;
using UnityEngine;

namespace ObjectPoolInternal
{
	public sealed class Pool
	{
		/// <summary>
		/// Where to store the master copy and all spawns
		/// </summary>
		private readonly Transform container;

		/// <summary>
		/// The master copy to create all spawns from
		/// </summary>
		private readonly GameObject master;

		/// <summary>
		/// The name of this cluster
		/// </summary>
		private readonly string name;

		/// <summary>
		/// List of currently unused spawns
		/// </summary>
		private Stack<GameObject> spawnable = new Stack<GameObject>();

		public Pool(GameObject master, string name)
		{
			this.name = name;

			if(master.scene.name == null)
				master = GameObject.Instantiate(master, container);

			this.master = master;

			master.name = name + " (master)";

			container = new GameObject(name).transform;
			container.SetParent(ObjectPool.Container);
			master.transform.position = Vector3.zero;

			master.transform.SetParent(container);

			master.SetActive(false);
		}

		/// <summary>
		/// Spawns an object from this cluster
		/// </summary>
		/// <returns></returns>
		public GameObject Rent()
		{
			GameObject spawn = null;

			if (spawnable.Count == 0)
			{
				spawn = GameObject.Instantiate(master, container);
				spawn.name = name;
			}
			else
				spawnable.Pop();

			spawn.SetActive(true);
			spawn.transform.SetParent(null);
			spawn.BroadcastMessage("OnSpawn", SendMessageOptions.DontRequireReceiver);

			return spawn;
		}

		/// <summary>
		/// Return a spawn to this cluster
		/// </summary>
		/// <param name="obj">The object to be returned</param>
		public void Return(GameObject obj)
		{
			obj.SetActive(false);
			obj.transform.SetParent(container);
			spawnable.Push(obj);
		}

		/// <summary>
		/// Destroyws any currently inactive copies
		/// </summary>
		public void Clean()
		{
			while(spawnable.Count > 0)
				GameObject.Destroy(spawnable.Pop());
		}

		/// <summary>
		/// Destroys all objects part of this pool, including currently active ones
		/// </summary>
		public void Clear()
		{
			GameObject.Destroy(container);
		}
	}
}
