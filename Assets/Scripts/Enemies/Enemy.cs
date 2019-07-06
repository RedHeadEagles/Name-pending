using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Entity
{
	private enum State
	{
		Wander,
		Attack,
		Return
	};

	public float speedChase = 10;

	public float speedWander = 3;

	public float aggroRange = 15;

	public float returnHomeRange = 30;

	private Entity target = null;

	protected Vector2 home;

	private State state = State.Wander;

    // Start is called before the first frame update
    void Start()
    {
		home = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
		var player = FindObjectOfType<Player>();

		if (DistanceTo(home) > 30)
			state = State.Return;

		switch(state)
		{
			case State.Attack:
				if (DistanceTo(player) > aggroRange)
					state = State.Return;
				else
					OnChase(target);
				break;

			case State.Wander:
				OnWander();
				if (DistanceTo(player) < aggroRange)
				{
					state = State.Attack;
					target = player;
				}
				break;

			case State.Return:
				OnReturnHome();
				if (DistanceTo(home) < 1)
					state = State.Wander;
				break;
		}
    }

	protected abstract void OnWander();

	protected abstract void OnChase(Entity target);

	protected abstract void OnReturnHome();
}
