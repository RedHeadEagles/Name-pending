using UnityEngine;

[CreateAssetMenu(menuName ="Items/Equipment/Backpack")]
public class Backpack : Equipment
{
	[Range(1, 60)]
	public uint slots = 1;
}
