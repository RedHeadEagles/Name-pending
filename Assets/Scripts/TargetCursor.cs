using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCursor : MonoBehaviour
{
	public bool targetMouse = true;

	public Vector3 target;

	public float angleOffset = 90;

    void Update()
    {
		if (targetMouse)
		{
			target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			target.z = 0;
		}

		var d = target - transform.position;
		float angle = Mathf.Atan2(d.y, d.x) * 180f / Mathf.PI;
		transform.localRotation = Quaternion.Euler(0, 0, angle + angleOffset);
	}
}
