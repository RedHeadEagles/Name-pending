using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
	public GameObject tracked;

	[Range(float.Epsilon, 100f)]
	public float trackSpeed = 1;

	public Vector3 offset = new Vector3(0, 0, -10);

	void Update()
	{
		if (tracked != null)
		{
			Vector3 end = tracked.transform.position + offset;
			transform.position = Vector3.Lerp(transform.position, end, trackSpeed * Time.unscaledDeltaTime);
		}
	}
}
