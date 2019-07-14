using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : Entity
{
	public Player player;

	public Item item;

	public Stamina stamina = new Stamina(100);

	public Slider healthBar;

	public Slider staminaBar;

	private Vector2 home;

	public float speed = 10;

	[Range(0f,1f)]
	public float time = 1;

	// Start is called before the first frame update
	void Start()
	{
		IO.WriteJson("player.json", player);
		home = transform.position;
	}

	// Update is called once per frame
	protected override void OnUpdate()
	{
		Time.timeScale = time;
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");
		Vector2 vector = new Vector2(x, y);
		Move(vector, speed);

		healthBar.value = health.Percentage;
		staminaBar.value = stamina.Percentage;
	}

	protected override void OnDeath()
	{
		transform.position = home;
		gameObject.SetActive(true);
		health.Reset();
	}
}
