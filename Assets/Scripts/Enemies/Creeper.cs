using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creeper : Enemy
{
	protected override void OnChase(Entity target)
	{
		Vector2 vector = target.transform.position - transform.position;
		vector.Normalize();
		Body.velocity = vector * speedChase;
	}

	protected override void OnWander()
	{
		Body.velocity = Vector2.zero;
	}
}
