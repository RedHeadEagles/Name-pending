using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Inventory))]
public class Player : Entity
{

	public float speed = 10;

	// Start is called before the first frame update
	void Start()
	{

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
		gameObject.SetActive(true);
	}
}
