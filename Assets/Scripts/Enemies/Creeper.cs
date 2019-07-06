using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creeper : Enemy
{
	public float wanderRange = 5;

	private float nextWander = 0;

	Vector2 wanderLocation;

	protected override void OnChase(Entity target)
	{
		MoveToward(target, speedChase);
	}

	protected override void OnWander()
	{
		if(nextWander<=0 || DistanceTo(wanderLocation) < 0.25f)
		{
			nextWander += 5;
			wanderLocation = Random.insideUnitCircle * wanderRange + home;
		}

		nextWander -= Time.deltaTime;
		MoveToward(wanderLocation, speedWander);
	}

	protected override void OnReturnHome()
	{
		MoveToward(home, speedChase * 2);
	}
}
