using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Entity : MonoBehaviour, IPathAgent
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

	private List<Vector3> path = new List<Vector3>();

	public bool PathFinding { get; private set; }

	private float nextPathFind = 0;

	[Range(float.Epsilon, 10f)]
	public float waypointDistance = 0.25f;

	/// <summary>
	/// Gets called whenever this script either gets added to a GameObject or Reset in the editor
	/// </summary>
	private void Reset()
	{
		inventory = GetComponent<Inventory>();

		rigidBody = GetComponent<Rigidbody2D>();
		rigidBody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
		rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
		rigidBody.drag = 10;

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

		nextPathFind -= Time.deltaTime;

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

	/// <summary>
	/// Performs a dodge roll in the given direction
	/// </summary>
	/// <param name="direction"></param>
	public void Dash(Vector2 direction)
	{
		rigidBody.AddForce(direction * stats.speed.Dash, ForceMode2D.Impulse);
	}

	/// <summary>
	/// Tells the entity to move in the given direction
	/// </summary>
	/// <param name="direction"></param>
	public void Move(Vector2 direction, float speedMod = 1)
	{
		rigidBody.AddForce(direction * stats.speed.Value * Time.deltaTime * speedMod, ForceMode2D.Impulse);
	}

	/// <summary>
	/// Tells the entity to move to the target location, will use pathfinding
	/// </summary>
	/// <param name="location"></param>
	public void MoveTo(Vector2 location, float speedMod = 1)
	{
		if (!PathFinding && nextPathFind <= 0 || !PathFinding && path.Count == 0)
		{
			PathFinding = true;
			AStar.FindPath(this, location);
		}

		if (path.Count > 0)
		{
			var dir = path[0] - transform.position;
			Move(dir.normalized, speedMod);

			if (Vector3.Distance(path[0], transform.position) < waypointDistance)
				path.RemoveAt(0);
		}
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
		stats.health.Current -= amount;
	}

	public void OnPathFound(List<Vector3> path, float pathTime)
	{
		nextPathFind = pathTime;
		this.path = path;

		PathFinding = false;
	}

	public virtual void OnPathFailed(float pathTime)
	{
		nextPathFind = pathTime;
		PathFinding = false;
	}

	protected virtual void OnDrawGizmosSelected()
	{
		var color = Gizmos.color;

		if(path.Count > 0)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawLine(transform.position, path[0]);

			for (int i = 1; i < path.Count; i++)
			{
				Gizmos.DrawWireSphere(path[i], 0.25f);
				Gizmos.DrawLine(path[i - 1], path[i]);
			}
		}

		Gizmos.color = color;
	}
}
