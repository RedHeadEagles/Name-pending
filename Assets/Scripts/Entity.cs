using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Entity : MonoBehaviour
{
	[SerializeField]
	protected CharacterData character;

	public CharacterData CharacterData
	{
		get { return character; }
		set
		{
			character = value;
			RecalculateStats();
		}
	}

	/// <summary>
	/// Represents how much health this entity has
	/// </summary>
	public readonly Stat health = new Stat(0.05f);

	/// <summary>
	/// Represents how much energy this entity has
	/// </summary>
	public readonly Stat energy = new Stat(0.05f);

	public readonly Stat energyRegen = new Stat(0.0001f);

	public readonly Stat defense = new Stat(0);

	/// <summary>
	/// How many units per second this entity can move
	/// </summary>
	public readonly Stat speed = new Stat(0.05f);

	/// <summary>
	/// How many units per secons this entity walks at
	/// </summary>
	public float WalkSpeed { get; protected set; }

	private Rigidbody2D body = null;

	protected float DistanceToPlayer { get; private set; }

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

	public bool IsAlive { get { return health.Current > 0; } }

	// Start is called before the first frame update
	void Awake()
	{
		Body.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
		Body.constraints = RigidbodyConstraints2D.FreezeRotation;

		health.Base = character.baseHealth;
		health.Level = character.stats.health;

		energy.Base = character.baseEnergy;
		energy.Level = character.stats.energy;

		energyRegen.Base = character.baseEnergyRegen;
		energyRegen.Level = character.stats.energyRegen;

		defense.Base = character.baseDefense;
		defense.Level = character.stats.defense;

		speed.Base = character.baseSpeed;
		speed.Level = character.stats.speed;
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

	public float ScaleStat(float baseValue, int level, float effect)
	{
		return baseValue * (1f + effect * level);
	}

	public void RecalculateStats()
	{
		health.Recalculate();
		energy.Recalculate();
		energyRegen.Recalculate();
		speed.Recalculate();
		WalkSpeed = speed.Current * character.walkSpeed;
		defense.Recalculate();
	}

	public void ApplyDamage(float amount)
	{
		health.Current -= amount;
	}
}
