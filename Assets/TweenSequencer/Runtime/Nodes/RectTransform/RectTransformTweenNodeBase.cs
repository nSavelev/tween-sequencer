using UnityEngine;

namespace TweenSequencer.Runtime
{
    public abstract class RectTransformTweenNodeBase : TweenOperationNode
    {
        [Input(backingValue = ShowBackingValue.Never)]
        public bool input;

        [Output(backingValue = ShowBackingValue.Never)]
        public bool next;

        public string targetParameterKey;

        protected RectTransform GetTarget(TweenScenarioContext context)
        {
            return context.GetParameter<RectTransform>(targetParameterKey);
        }

        protected Vector2 ResolveVector2(TweenScenarioContext context, string parameterKey, Vector2 fallback)
        {
            if (string.IsNullOrWhiteSpace(parameterKey) || !context.TryGetParameter(parameterKey, out var value) ||
                value == null) return fallback;

            if (value is Vector2 vector2) return vector2;
            if (value is Vector3 vector3) return new Vector2(vector3.x, vector3.y);
            if (value is RectTransform rect) return rect.anchoredPosition;
            return fallback;
        }

        protected Vector3 ResolveVector3(TweenScenarioContext context, string parameterKey, Vector3 fallback)
        {
            if (string.IsNullOrWhiteSpace(parameterKey) || !context.TryGetParameter(parameterKey, out var value) ||
                value == null) return fallback;

            if (value is Vector3 vector3) return vector3;
            if (value is Vector2 vector2) return new Vector3(vector2.x, vector2.y, fallback.z);
            if (value is RectTransform rect) return rect.anchoredPosition3D;
            return fallback;
        }

        protected float ResolveFloat(TweenScenarioContext context, string parameterKey, float fallback)
        {
            if (string.IsNullOrWhiteSpace(parameterKey)) return fallback;
            return context.GetParameter(parameterKey, fallback);
        }
    }
}