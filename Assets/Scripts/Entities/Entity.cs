using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Entity : MonoBehaviour
{
	/// <summary>
	/// Gets this entity's inventory, if it has one
	/// </summary>
	public Inventory inventory { get { return GetComponent<Inventory>(); } }

	/// <summary>
	/// Gets the AI script this entity is using, if it has one
	/// </summary>
	public AI ai { get { return GetComponent<AI>(); } }

	public Rigidbody2D rigidBody { get { return GetComponent<Rigidbody2D>(); } }

	public Health health = new Health();

	public Stamina stamina = new Stamina();

	/// <summary>
	/// Is this entity currently considered dead
	/// </summary>
	public bool IsDead { get { return !IsAlive; } }

	/// <summary>
	/// Is this entity currently considered to be alive
	/// </summary>
	public virtual bool IsAlive { get { return health.Current > 0; } }

	// Start is called before the first frame update
	void Awake()
	{
		rigidBody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
		rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
	}

	// Update is called once per frame
	void Update()
	{

		if (false)
		{
			gameObject.SetActive(false);
			OnDeath();
			return;
		}

		//health.DoRegen(Time.deltaTime);

		OnUpdate();
	}

	protected virtual void OnUpdate() { }

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
		rigidBody.velocity = vector * speed;
	}

	public float DistanceTo(Entity entity)
	{
		return Vector2.Distance(transform.position, entity.transform.position);
	}

	public float DistanceTo(Vector2 location)
	{
		return Vector2.Distance(transform.position, location);
	}

	public void ApplyDamage(float amount)
	{
		Debug.Log(string.Format("{0} took {1} damage!", name, amount));
		health.Current -= amount;
	}
}
