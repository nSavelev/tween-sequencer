using System.Collections.Generic;
using TweenSequencer.Runtime;
using UnityEditor;
using UnityEngine;

namespace TweenSequencer.Editor
{
    [CustomPropertyDrawer(typeof(ParameterKeyAttribute))]
    public class ParameterKeyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.String)
            {
                EditorGUI.PropertyField(position, property, label);
                return;
            }

            var keys = TweenScenarioEditorUtils.GetAllAvailableParameterKeys();
            var options = new List<string>(keys);
            options.Insert(0, "<None>");

            var currentIndex = 0;
            for (var i = 0; i < keys.Count; i++)
                if (keys[i] == property.stringValue)
                {
                    currentIndex = i + 1;
                    break;
                }

            var popupRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            var selected = EditorGUI.Popup(popupRect, label.text, currentIndex, options.ToArray());
            if (selected == 0) property.stringValue = string.Empty;
            else property.stringValue = keys[selected - 1];
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }
    }
}