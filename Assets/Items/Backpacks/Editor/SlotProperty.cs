using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Slot))]
public class SlotProperty : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		EditorGUI.BeginProperty(position, label, property);

		EditorGUI.indentLevel++;

		var amountRect = new Rect(position.x, position.y, 70, position.height);
		var itemRect = new Rect(position.x + 75, position.y, position.width - 75, position.height);

		EditorGUI.PropertyField(amountRect, property.FindPropertyRelative("quantity"), GUIContent.none);
		EditorGUI.PropertyField(itemRect, property.FindPropertyRelative("item"), GUIContent.none);

		EditorGUI.indentLevel--;

		EditorGUI.EndProperty();
	}
}
