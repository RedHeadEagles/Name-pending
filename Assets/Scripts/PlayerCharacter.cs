using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : Entity
{
	public Stamina stamina = new Stamina(100);

	public Slider healthBar;

	public Slider staminaBar;

	private Vector2 home;

	public float speed = 10;

	public Vector2 mouse;

	float next;

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

		healthBar.value = health.Percentage;
		staminaBar.value = stamina.Percentage;

		next -= Time.deltaTime;
		if (next <= 0)
		{
			next = 0.5f;
			mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			MoveToward(mouse, speed);
		}
	}

	protected override void OnDeath()
	{
		transform.position = home;
		gameObject.SetActive(true);
		health.Reset();
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(mouse, 0.5f);

		if (path == null || path.Count == 0)
			return;

		Color color = Gizmos.color;
		Gizmos.color = Color.red;

		Gizmos.DrawLine(transform.position, path[0]);

		for (int i = 1; i < path.Count; i++)
			Gizmos.DrawLine(path[i - 1], path[i]);

		Gizmos.color = color;
	}
}
