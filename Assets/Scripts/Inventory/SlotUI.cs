using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
	public Image icon;

	public Text quantity;

	private void OnMouseDown()
	{
		Debug.Log(gameObject.name);
	}
}
