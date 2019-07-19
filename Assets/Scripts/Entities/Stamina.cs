using UnityEngine;

[System.Serializable]
public class Stamina
{
	public const float LevelModifier = 0.05f;

	public float baseValue = 100;

	private int level = 0;

	public int Level
	{
		get { return level; }
		set
		{
			level = value;
			Max = baseValue * (1f + LevelModifier * level);
		}
	}

	public float Max { get; set; }

	private float current;

	public float Current
	{
		get { return current; }
		set { current = Mathf.Clamp(value, 0, Max); }
	}

	public float Percentage { get { return Current / Max; } }

	public void Reset()
	{
		current = Max;
	}
}
