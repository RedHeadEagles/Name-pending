using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Entity : MonoBehaviour
{
	public Health health = new Health();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (health.IsDead)
		{
			gameObject.SetActive(false);
			OnDeath();
		}
    }

	/// <summary>
	/// Called when this entity is killed
	/// </summary>
	protected virtual void OnDeath() { }
}
