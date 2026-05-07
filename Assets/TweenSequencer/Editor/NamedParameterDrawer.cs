using TweenSequencer.Runtime;
using UnityEditor;
using UnityEngine;

namespace TweenSequencer.Editor
{
    [CustomPropertyDrawer(typeof(NamedParameter))]
    public class NamedParameterDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var keyProp = property.FindPropertyRelative("key");
            var typeProp = property.FindPropertyRelative("type");
            var valueProp = GetValueProperty(property, (NamedParameterType)typeProp.enumValueIndex);

            var line = EditorGUIUtility.singleLineHeight;
            var spacing = EditorGUIUtility.standardVerticalSpacing;

            var keyRect = new Rect(position.x, position.y, position.width, line);
            var typeRect = new Rect(position.x, keyRect.yMax + spacing, position.width, line);
            var valueRect = new Rect(position.x, typeRect.yMax + spacing, position.width, line);

            EditorGUI.PropertyField(keyRect, keyProp);
            EditorGUI.PropertyField(typeRect, typeProp);
            if (valueProp != null) EditorGUI.PropertyField(valueRect, valueProp, new GUIContent("Value"));

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var line = EditorGUIUtility.singleLineHeight;
            var spacing = EditorGUIUtility.standardVerticalSpacing;
            return line * 3f + spacing * 2f;
        }

        private static SerializedProperty GetValueProperty(SerializedProperty property, NamedParameterType type)
        {
            switch (type)
            {
                case NamedParameterType.String:
                    return property.FindPropertyRelative("stringValue");
                case NamedParameterType.Int:
                    return property.FindPropertyRelative("intValue");
                case NamedParameterType.Float:
                    return property.FindPropertyRelative("floatValue");
                case NamedParameterType.Bool:
                    return property.FindPropertyRelative("boolValue");
                case NamedParameterType.Vector2:
                    return property.FindPropertyRelative("vector2Value");
                case NamedParameterType.Vector3:
                    return property.FindPropertyRelative("vector3Value");
                case NamedParameterType.Color:
                    return property.FindPropertyRelative("colorValue");
                case NamedParameterType.Object:
                    return property.FindPropertyRelative("objectValue");
                default:
                    return null;
            }
        }
    }
}