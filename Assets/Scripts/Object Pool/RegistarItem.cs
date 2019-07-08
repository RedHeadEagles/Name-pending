using UnityEngine;

namespace ObjectPoolInternal
{
	[System.Serializable]
	public struct RegistarItem
	{
		[SerializeField]
		private string name;

		public string Name
		{
			get
			{
				if (name == null || name == "")
					return obj.name;
				return name;
			}
		}

		[SerializeField]
		private GameObject obj;

		public GameObject Obj
		{
			get
			{
				return obj;
			}
		}
	}
}