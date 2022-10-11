using System;
using UnityEditor;
using UnityEngine;

namespace ValuePlus.Editor
{
    [CustomPropertyDrawer(typeof(RangeAttribute))]
    public class RangeAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            
            
            if (!ValidatePropertyType(property))
            {
                EditorGUI.LabelField(position, label, new GUIContent("Use ValuePlus.RangeAttribute only for FloatRange and IntRange types."));
                return;
            }

            bool isInt = property.type == nameof(IntRange);
            
            RangeAttribute rangeAttribute = attribute as RangeAttribute;

            var minVal = property.FindPropertyRelative("_min");
            var maxVal = property.FindPropertyRelative("_max");
            
            float min = isInt ? minVal.intValue : minVal.floatValue;
            float max = isInt ? maxVal.intValue : maxVal.floatValue;

            Rect rangeRect = position;
            rangeRect.height = EditorGUIUtility.singleLineHeight;

            Rect linesRect = rangeRect;
            linesRect.x += EditorGUIUtility.labelWidth + 8;
            linesRect.width -= EditorGUIUtility.labelWidth + 16;
            int count = (int)(rangeAttribute.max - rangeAttribute.min);
            if (Event.current.type == EventType.Repaint && isInt && linesRect.width / count > 8)
            {
                var material = new Material(Shader.Find("Hidden/Internal-Colored"));
                GUI.BeginClip(linesRect);
                GL.PushMatrix();
                
                GL.Clear(true, false, Color.black);
                material.SetPass(0);
                
                GL.Begin(GL.LINES);
                GL.Color(new Color(132f/255, 132f/255, 132f/255));

                
                for (int i = 0; i <= rangeAttribute.max - rangeAttribute.min; i++)
                {
                    float width = linesRect.width;
                    GL.Vertex3(i * (width / count), 0, 0);
                    GL.Vertex3(i * (width / count), linesRect.height, 0);
                }

                GL.End();
                
                GL.PopMatrix();
                GUI.EndClip();
            }
            
            EditorGUI.BeginChangeCheck();
            EditorGUI.MinMaxSlider(rangeRect, label, ref min, ref max, rangeAttribute.min, rangeAttribute.max);


            if (EditorGUI.EndChangeCheck())
            {
                if (isInt)
                {
                    minVal.intValue = Mathf.RoundToInt(Mathf.Clamp(min, rangeAttribute.min, max));
                    maxVal.intValue = Mathf.RoundToInt(Mathf.Clamp(max, min, rangeAttribute.max));
                }
                else
                {
                    minVal.floatValue = Mathf.Clamp((float)Math.Round(min, 2, MidpointRounding.AwayFromZero), rangeAttribute.min, max);
                    maxVal.floatValue = Mathf.Clamp((float)Math.Round(max, 2, MidpointRounding.AwayFromZero), min, rangeAttribute.max);
                }
            }

            rangeAttribute.isOpened = EditorGUI.Foldout(rangeRect, rangeAttribute.isOpened, GUIContent.none);
            if (!rangeAttribute.isOpened)
            {
                return;
            }
            
            Rect minFieldRect = rangeRect;
            minFieldRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            minFieldRect.width = EditorGUIUtility.currentViewWidth * 0.2f;
            minFieldRect.x += EditorGUIUtility.labelWidth;

            Rect maxFieldRect = minFieldRect;
            maxFieldRect.x = EditorGUIUtility.currentViewWidth - EditorGUIUtility.currentViewWidth * 0.2f - 10f;

            EditorGUIUtility.labelWidth = 30;
            
            EditorGUI.BeginChangeCheck();
            
            min = isInt ? 
                EditorGUI.IntField(minFieldRect, "Min", (int)min, EditorStyles.miniTextField) : 
                EditorGUI.FloatField(minFieldRect, "Min", min, EditorStyles.miniTextField);
            
            if (EditorGUI.EndChangeCheck())
            {
                if (isInt)
                {
                    minVal.intValue = (int)Mathf.Clamp(min, rangeAttribute.min, max);
                }
                else
                {
                    minVal.floatValue = Mathf.Clamp(min, rangeAttribute.min, max);
                }
                
            }
            EditorGUI.BeginChangeCheck();
            
            max = isInt ? 
                EditorGUI.IntField(maxFieldRect, "Max", (int)max, EditorStyles.miniTextField) :
                EditorGUI.FloatField(maxFieldRect, "Max", max, EditorStyles.miniTextField);
            
            if (EditorGUI.EndChangeCheck())
            {
                if (isInt)
                {
                    maxVal.intValue = (int)Mathf.Clamp(max, min, rangeAttribute.max);
                }
                else
                {
                    maxVal.floatValue = Mathf.Clamp(max, min, rangeAttribute.max);
                }
            }
        }

        private static bool ValidatePropertyType(SerializedProperty property)
        {
            return property.type is nameof(FloatRange) or nameof(IntRange);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!ValidatePropertyType(property) || !((RangeAttribute)attribute).isOpened)
            {
                return base.GetPropertyHeight(property, label);
            }
            return (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) * 2;
        }
    }
}