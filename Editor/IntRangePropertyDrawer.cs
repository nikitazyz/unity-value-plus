using UnityEditor;
using UnityEngine;

namespace ValuePlus.Editor
{
    [CustomPropertyDrawer(typeof(IntRange))]
    public class IntRangePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Rect labelPos = position;
            labelPos.height = EditorGUIUtility.singleLineHeight;
            Rect minPos = labelPos;
            minPos.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            Rect maxPos = minPos;
            maxPos.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            var minVal = property.FindPropertyRelative("_min");
            var maxVal = property.FindPropertyRelative("_max");

            EditorGUI.LabelField(labelPos, label);
            EditorGUI.indentLevel++;
            EditorGUI.PropertyField(minPos, minVal);
            EditorGUI.PropertyField(maxPos, maxVal);
            EditorGUI.indentLevel--;
        }
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) * 3;
        }
    }
}