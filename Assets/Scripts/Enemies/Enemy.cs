using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Entity
{
	public float speedChase = 10;

	public float speedWander = 3;

	private Entity target = null;

	protected Vector2 home;

    // Start is called before the first frame update
    void Start()
    {
		home = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
		if (target != null)
			OnChase(target);
		else
			OnWander();
    }

	protected abstract void OnWander();

	protected abstract void OnChase(Entity target);

	private void OnTriggerExit2D(Collider2D collision)
	{
		Entity entity = collision.gameObject.GetComponent<Entity>();

		if (entity == null || !(entity is Player))
			return;

		target = null;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Entity entity = collision.gameObject.GetComponent<Entity>();

		if (entity == null || !(entity is Player))
			return;

		target = entity;
	}
}
