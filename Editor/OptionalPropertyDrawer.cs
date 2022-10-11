using UnityEditor;
using UnityEngine;

namespace ValuePlus.Editor
{
    [CustomPropertyDrawer(typeof(Optional<>))]
    public class OptionalPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var value = property.FindPropertyRelative("_value");
            var enabled = property.FindPropertyRelative("_isEnabled");

            Rect toggleRect = position;
            toggleRect.width = 16;
            toggleRect.height = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            Rect valueRect = position;
            enabled.boolValue = EditorGUI.Toggle(toggleRect, enabled.boolValue);
            GUI.enabled = enabled.boolValue;
            EditorGUI.indentLevel+=2;
            EditorGUI.PropertyField(valueRect, value, label, true);
            EditorGUI.indentLevel+=2;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var value = property.FindPropertyRelative("_value");
            return EditorGUI.GetPropertyHeight(value);
        }
    }
}