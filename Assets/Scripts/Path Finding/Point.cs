using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Point
{
	public int x;

	public int y;

	public Point(int X, int Y)
	{
		x = X;
		y = Y;
	}

	public float Distance(Point a, Point b)
	{

		return Mathf.Sqrt(Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y));
	}

	public static Point operator +(Point a, Point b)
	{
		return new Point(a.x + b.x, a.y + b.y);
	}

	public static Point operator -(Point a, Point b)
	{
		return new Point(a.x - b.x, a.y - b.y);
	}

	public static Point operator *(Point a, int mod)
	{
		return new Point(a.x * mod, a.y * mod);
	}
}
