using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Inventory))]
public class Player : Entity
{
	public Character character;

	// Update is called once per frame
	protected override void Update()
	{
		Debug.Log(character.worldSprites.animations[0].name);
		base.Update();

		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");
		Vector2 vector = new Vector2(x, y);
		Move(vector);

		if (Input.GetKeyDown(KeyCode.R))
			Dash(vector.normalized);
	}

	protected override void OnDeath()
	{
		gameObject.SetActive(true);
	}
}
