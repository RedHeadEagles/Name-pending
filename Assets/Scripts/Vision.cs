using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
	public LayerMask terrainLayer;

	private List<Entity> visable = new List<Entity>();

    void Update()
	{

		foreach (var item in visable)
		{
			Debug.Log(item);
		}
		visable.Clear();
    }

	public bool CanSee(Entity entity)
	{
		return visable.Contains(entity);
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		var entity = collision.GetComponent<Entity>();
		if (entity == null)
			return;

		var vector = entity.transform.position - transform.position;
		var hit = Physics2D.Raycast(transform.position, vector, vector.magnitude, terrainLayer);

		if (hit.collider == null)
			visable.Add(entity);
	}
}
