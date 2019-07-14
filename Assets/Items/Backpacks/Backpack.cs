using UnityEngine;

[CreateAssetMenu]
public class Backpack : Item
{
	[Range(1, 60)]
	public uint slots = 1;
}
