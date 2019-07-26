using UnityEngine;
using System.Collections;

[System.Serializable]
public struct Point
{
	public int x;

	public int y;

	public static implicit operator Point(Vector2Int vector)
	{
		return new Point()
		{
			x = vector.x,
			y = vector.y
		};
	}

	public static bool operator ==(Point a, Point b)
	{
		return a.x == b.x && a.y == b.y;
	}

	public static bool operator !=(Point a, Point b)
	{
		return a.x != b.x || a.y != b.y;
	}
}
