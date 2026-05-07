using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TweenSequencer.Runtime
{
    public enum NamedParameterType
    {
        String,
        Int,
        Float,
        Bool,
        Vector2,
        Vector3,
        Color,
        Object,
        Action,
        CoroutineFactory
    }

    [Serializable]
    public class NamedParameter
    {
        public string key;
        public NamedParameterType type;
        public string stringValue;
        public int intValue;
        public float floatValue;
        public bool boolValue;
        public Vector2 vector2Value;
        public Vector3 vector3Value;
        public Color colorValue = Color.white;

        public Object objectValue;

        // runtime-only helpers
        [NonSerialized]
        public Action actionValue;

        [NonSerialized]
        public Func<IEnumerator> coroutineFactoryValue;

        public object GetValue()
        {
            switch (type)
            {
                case NamedParameterType.String: return stringValue;
                case NamedParameterType.Int: return intValue;
                case NamedParameterType.Float: return floatValue;
                case NamedParameterType.Bool: return boolValue;
                case NamedParameterType.Vector2: return vector2Value;
                case NamedParameterType.Vector3: return vector3Value;
                case NamedParameterType.Color: return colorValue;
                case NamedParameterType.Object: return objectValue;
                case NamedParameterType.Action: return actionValue;
                case NamedParameterType.CoroutineFactory: return coroutineFactoryValue;
                default: return null;
            }
        }

        public static NamedParameter FromAction(string key, Action action)
        {
            return new NamedParameter
            {
                key = key,
                type = NamedParameterType.Action,
                actionValue = action
            };
        }

        public static NamedParameter FromCoroutineFactory(string key, Func<IEnumerator> coroutineFactory)
        {
            return new NamedParameter
            {
                key = key,
                type = NamedParameterType.CoroutineFactory,
                coroutineFactoryValue = coroutineFactory
            };
        }

        // Basic typed factories
        public static NamedParameter FromString(string key, string value)
        {
            return new NamedParameter { key = key, type = NamedParameterType.String, stringValue = value };
        }

        public static NamedParameter FromInt(string key, int value)
        {
            return new NamedParameter { key = key, type = NamedParameterType.Int, intValue = value };
        }

        public static NamedParameter FromFloat(string key, float value)
        {
            return new NamedParameter { key = key, type = NamedParameterType.Float, floatValue = value };
        }

        public static NamedParameter FromBool(string key, bool value)
        {
            return new NamedParameter { key = key, type = NamedParameterType.Bool, boolValue = value };
        }

        public static NamedParameter FromVector2(string key, Vector2 value)
        {
            return new NamedParameter { key = key, type = NamedParameterType.Vector2, vector2Value = value };
        }

        public static NamedParameter FromVector3(string key, Vector3 value)
        {
            return new NamedParameter { key = key, type = NamedParameterType.Vector3, vector3Value = value };
        }

        public static NamedParameter FromColor(string key, Color value)
        {
            return new NamedParameter { key = key, type = NamedParameterType.Color, colorValue = value };
        }

        public static NamedParameter FromObject(string key, Object value)
        {
            return new NamedParameter { key = key, type = NamedParameterType.Object, objectValue = value };
        }
    }
}