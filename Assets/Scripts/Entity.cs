using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Entity : MonoBehaviour
{
	
	private Rigidbody2D body = null;

	protected float DistanceToPlayer { get; private set; }

	[SerializeField]
	private LayerMask terrain;

	public Rigidbody2D Body
	{
		get
		{
			if (body == null)
				body = GetComponent<Rigidbody2D>();

			return body;
		}
	}

	public bool IsDead { get { return false; } }

	public bool ISAlive { get { return true; } }

	// Start is called before the first frame update
	void Awake()
	{
		Body.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
		Body.constraints = RigidbodyConstraints2D.FreezeRotation;
	}

	// Update is called once per frame
	void Update()
	{
		DistanceToPlayer = GameManager.Player.IsDead ? float.MaxValue : DistanceTo(GameManager.Player);

		if (IsDead)
		{
			gameObject.SetActive(false);
			OnDeath();
			return;
		}

		//health.DoRegen(Time.deltaTime);

		OnUpdate();
	}

	protected abstract void OnUpdate();

	/// <summary>
	/// Called when this entity is killed
	/// </summary>
	protected virtual void OnDeath() { }

	public void MoveToward(Entity entity, float speed)
	{
		MoveToward(entity.transform.position, speed);
	}

	public void MoveToward(Vector3 location, float speed)
	{
		Vector2 vector = location - transform.position;
		Move(vector.normalized, speed);
	}

	public void Move(Vector3 vector, float speed)
	{
		body.velocity = vector * speed;
	}

	public float DistanceTo(Entity entity)
	{
		return Vector2.Distance(transform.position, entity.transform.position);
	}

	public float DistanceTo(Vector2 location)
	{
		return Vector2.Distance(transform.position, location);
	}
}
