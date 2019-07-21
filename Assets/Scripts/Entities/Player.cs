using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Inventory))]
public class Player : Entity
{
	// Update is called once per frame
	protected override void Update()
	{
		base.Update();

		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");
		Vector2 vector = new Vector2(x, y);
		Move(vector, stats.speed.Value);
	}

	protected override void OnDeath()
	{
		gameObject.SetActive(true);
	}
}
