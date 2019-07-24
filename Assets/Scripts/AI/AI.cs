using UnityEngine;

[RequireComponent(typeof(Entity))]
public abstract class AI : MonoBehaviour
{
	private Entity _entity;

	public Entity entity
	{
		get
		{
			if (_entity == null)
				_entity = GetComponent<Entity>();

			return _entity;
		}
	}
}
