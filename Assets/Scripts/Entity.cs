using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Entity : MonoBehaviour, IPathAgent
{
	public Health health = new Health(100);

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

	public bool IsDead { get { return health.Current == 0; } }

	public bool IsNotDead { get { return health.Current > 0; } }

	private bool pathfinding = false;

	public List<Vector3> path = new List<Vector3>();

	private float nextPathRequest = 0;

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

		if (health.IsDead)
		{
			gameObject.SetActive(false);
			OnDeath();
			return;
		}

		if(path.Count>0)
		{
			var dir = path[0] - transform.position;
		}

		health.DoRegen(Time.deltaTime);

		nextPathRequest -= Time.deltaTime;

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
		if (nextPathRequest <= 0)
		{
			nextPathRequest = 0.5f;
			AStar.FindPath(this, location);
		}
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

	public void OnPathFound(List<Vector3> path)
	{
		this.path = path;
	}

	public virtual void OnPathFailed() { }
}
