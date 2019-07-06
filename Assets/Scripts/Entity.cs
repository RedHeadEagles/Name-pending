using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Entity : MonoBehaviour
{
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

	public void Move(Vector2 vector, float speed)
	{
		Body.velocity = vector * speed;
	}
}
