using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Entity : MonoBehaviour
{
	/// <summary>
	/// Gets this entity's inventory, if it has one
	/// </summary>
	public Inventory inventory;

	/// <summary>
	/// Gets the AI script this entity is using, if it has one
	/// </summary>
	public AI ai;

	public Rigidbody2D rigidBody;

	public Stats stats = new Stats();

	/// <summary>
	/// Provides quick access to the health stat
	/// </summary>
	public Health Health { get { return stats.health; } }

	/// <summary>
	/// Provides quick access to the stamina stat
	/// </summary>
	public Stamina Stamina { get { return stats.stamina; } }

	/// <summary>
	/// Is this entity currently considered dead
	/// </summary>
	public bool IsDead { get { return !IsAlive; } }

	/// <summary>
	/// Is this entity currently considered to be alive
	/// </summary>
	public virtual bool IsAlive { get { return stats.health.Current > 0; } }

	/// <summary>
	/// Gets called whenever this script either gets added to a GameObject or Reset in the editor
	/// </summary>
	private void Reset()
	{
		inventory = GetComponent<Inventory>();

		rigidBody = GetComponent<Rigidbody2D>();
		rigidBody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
		rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;

		ai = GetComponent<AI>();
	}

	protected virtual void Start()
	{
		OnSpawn();
	}

	public virtual void OnSpawn()
	{
		Health.Reset();
		Stamina.Reset();
	}
	
	protected virtual void Update()
	{
		if(IsDead)
		{
			OnDeath();
			return;
		}

		Stamina.DoRegen(Time.deltaTime);
	}

	/// <summary>
	/// Called when this entity is killed
	/// </summary>
	protected virtual void OnDeath()
	{
		Debug.Log(gameObject.name + " has died!");
		gameObject.SetActive(false);
	}

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
		stats.health.Current -= amount;
	}
}
