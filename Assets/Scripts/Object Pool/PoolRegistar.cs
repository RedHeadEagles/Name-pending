using UnityEngine;
using System.Collections.Generic;

namespace ObjectPoolInternal
{
	/// <summary>
	/// Automatically registers objects into the object pool
	/// </summary>
	public class PoolRegistar : MonoBehaviour
	{
		public List<RegistarItem> registars = new List<RegistarItem>();

		private void Awake()
		{
			foreach (var item in registars)
				ObjectPool.Register(item.name, item.gameObject);
		}
	}
}