using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Inventory))]
public class Player : Entity
{
	private PlayerData playerData;

	public ItemData item;

	public Slider healthBar;

	public Slider staminaBar;

	public float speed = 10;

	public Inventory Inventory { get { return GetComponent<Inventory>(); } }

	// Start is called before the first frame update
	void Start()
	{
		playerData = character as PlayerData;
		transform.position = Zone.Current.playerSpawnLocation;
		Debug.Log(health.Max);
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
