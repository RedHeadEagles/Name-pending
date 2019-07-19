using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : Entity
{
	private enum State
	{
		Wander,
		Chase,
		Return,
		Attack
	};

	public float wanderRange = 5;

	private float nextWander = 0;

	Vector2 wanderLocation;

	public float speedChase = 10;

	public float speedWander = 3;

	public float aggroRange = 15;

	public float returnHomeRange = 30;

	public float wanderTime = 5;

	public float attackRange = 1;

	public float attackTime = 2;

	public float attackDamage = 10;

	private float nextAttack = 0;

	private float moveLock = 0;

	private Entity target = null;

	protected Vector2 home;

	private State state = State.Wander;

	// Start is called before the first frame update
	void Start()
	{
		home = transform.position;
	}

	// Update is called once per frame
	protected override void OnUpdate()
	{
		// Return home if we have strayed to far from our spawn location
		if (DistanceTo(home) > 30)
			state = State.Return;

		switch (state)
		{
			case State.Chase:
				nextAttack -= Time.deltaTime;

				if (moveLock > 0)
				{
					moveLock -= Time.deltaTime;
					break;
				}
				else if (DistanceTo(GameManager.Player) < attackRange)
					state = State.Attack;
				else if (DistanceTo(GameManager.Player) > aggroRange)
					state = State.Return;
				else
					MoveToward(GameManager.Player, speedChase);
				break;

			case State.Wander:
				nextWander -= Time.deltaTime / 2;

				if (nextWander <= 0)
				{
					nextWander += Random.Range(0f, wanderTime);
					wanderLocation = Random.insideUnitCircle * wanderRange + home;
				}

				MoveToward(wanderLocation, speedWander);

				if (DistanceTo(GameManager.Player) < aggroRange)
				{
					nextAttack = 0.5f;
					state = State.Chase;
					target = GameManager.Player;
				}
				break;

			case State.Attack:
				nextAttack -= Time.deltaTime;

				if (DistanceTo(GameManager.Player) > attackRange)
					state = State.Chase;

				if (nextAttack <= 0)
				{
					nextAttack = attackTime;
					moveLock = 0.25f;
				}

				rigidBody.velocity = Vector2.zero;
				break;

			case State.Return:
				MoveToward(home, speedChase);
				if (DistanceTo(home) < 1)
					state = State.Wander;
				break;
		}
	}
}
