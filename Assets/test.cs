using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : Entity
{
	public List<Entity> seen = new List<Entity>();

	protected override void Update()
	{
		base.Update();
		seen.Clear();
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		var entity = collision.GetComponent<Entity>();
		if (entity == null)
			return;

		var hit = Physics2D.Raycast(transform.position, entity.transform.position - transform.position);
		
		if (hit.collider.GetComponent<Entity>() != entity)
			return;

		seen.Add(entity);
	}
}
