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

	private List<Vector2> path = new List<Vector2>();

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

		//path = Terrain.FindPath(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
	}

	protected override void OnDeath()
	{
		transform.position = home;
		gameObject.SetActive(true);
		health.Reset();
	}

	private void OnDrawGizmos()
	{
		if (path == null)
			return;

		Color color = Gizmos.color;
		Gizmos.color = Color.red;

		for (int i = 1; i < path.Count; i++)
		{
			Gizmos.DrawLine(path[i - 1], path[i]);
		}

		Gizmos.color = color;
	}
}
