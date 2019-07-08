using UnityEngine;
using UnityEditor;

namespace ObjectPoolInternal
{
	[CanEditMultipleObjects]
	[CustomPropertyDrawer(typeof(RegistarItem))]
	public class RegistarItemPropertyDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);

			// Calculate rects
			var nameRect = new Rect(position.x, position.y, position.width / 5 * 3, position.height);
			var objectRect = new Rect(nameRect.x + nameRect.width, position.y, position.width / 5 * 2, position.height);

			// Draw fields - passs GUIContent.none to each so they are drawn without labels
			EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("name"), GUIContent.none);
			EditorGUI.PropertyField(objectRect, property.FindPropertyRelative("obj"), GUIContent.none);

			EditorGUI.EndProperty();
		}
	}
}