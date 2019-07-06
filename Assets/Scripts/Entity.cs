using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Entity : MonoBehaviour
{
	[SerializeField]
	protected int maxHealth = 100;

	public int MaxHealth { get { return maxHealth; } }

	private int healthCurrent = 100;
	public Health health = new Health(100);

	private Rigidbody2D body = null;

	public Rigidbody2D Body
	{
		get
		{
			if (body == null)
				body = GetComponent<Rigidbody2D>();

			return body;
		}
	}

    // Start is called before the first frame update
    void Awake()
    {
		Body.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
		Body.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    // Update is called once per frame
    void Update()
    {
		health.DoRegen(Time.deltaTime);

		if (health.IsDead)
		{
			OnDeath();
			gameObject.SetActive(false);
		}
    }

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

	public void Move(Vector2 vector, float speed)
	{
		Body.velocity = vector * speed;
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
