using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
	public Stamina stamina = new Stamina(100);

	private Vector2 home;

	public float speed = 10;

    // Start is called before the first frame update
    void Start()
    {
		home = transform.position;
    }

	// Update is called once per frame
	protected override void OnUpdate()
    {
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");
		Vector2 vector = new Vector2(x, y);
		Move(vector, speed);
	}

	protected override void OnDeath()
	{
		transform.position = home;
		gameObject.SetActive(true);
		health.Reset();
	}
}
