﻿using UnityEngine;

/// <summary>
/// Forces a single instance of a class at a single time
/// </summary>
public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T instance = null;

	public static T Instance
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<T>();

				if (instance == null)
				{
					GameObject obj = new GameObject(typeof(T).Name);

					instance = obj.AddComponent<T>();
				}
			}

			return instance;
		}
	}

	private void Awake()
	{
		if (Instance != this)
		{
			Destroy(this);
			return;
		}

		OnFirstRun();
	}

	protected virtual void OnFirstRun()
	{

	}
}