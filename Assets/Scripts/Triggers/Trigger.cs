using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Trigger : MonoBehaviour
{
	public TriggerTargets targets = new TriggerTargets();

	private bool IsAllowedTarget(Entity entity)
	{
		if (entity is Player && targets.player)
			return true;

		if (entity is Boss && targets.boss)
			return true;

		if (targets.entity)
			return true;

		return false;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Entity entity = collision.GetComponent<Entity>();

		if (entity == null || !IsAllowedTarget(entity))
			return;

		OnEnter(entity);
	}

	/// <summary>
	/// Called when an entity enters the trigger
	/// </summary>
	/// <param name="entity">Reference to the entity that entered</param>
	protected virtual void OnEnter(Entity entity) { }

	private void OnTriggerExit2D(Collider2D collision)
	{
		Entity entity = collision.GetComponent<Entity>();

		if (entity == null || !IsAllowedTarget(entity))
			return;

		OnExit(entity);
	}

	/// <summary>
	/// Called when an entity exits the trigger
	/// </summary>
	/// <param name="entity">Reference to the entity that left</param>
	protected virtual void OnExit(Entity entity) { }
}
