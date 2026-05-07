using UnityEngine;

namespace TweenSequencer.Runtime
{
    public abstract class TransformTweenNodeBase : TweenOperationNode
    {
        [Input(backingValue = ShowBackingValue.Never)]
        public bool input;

        [Output(backingValue = ShowBackingValue.Never)]
        public bool next;

        public string targetParameterKey;

        protected Transform GetTarget(TweenScenarioContext context)
        {
            return context.GetParameter<Transform>(targetParameterKey);
        }

        protected Vector3 ResolveVector3(TweenScenarioContext context, string parameterKey, Vector3 fallback,
            bool useTransformPosition, bool useTransformEuler)
        {
            if (string.IsNullOrWhiteSpace(parameterKey) || !context.TryGetParameter(parameterKey, out var value) ||
                value == null) return fallback;

            if (value is Vector3 vector) return vector;

            if (value is Transform transform)
            {
                if (useTransformPosition) return transform.position;

                if (useTransformEuler) return transform.rotation.eulerAngles;
            }

            return fallback;
        }

        protected float ResolveFloat(TweenScenarioContext context, string parameterKey, float fallback)
        {
            if (string.IsNullOrWhiteSpace(parameterKey)) return fallback;

            return context.GetParameter(parameterKey, fallback);
        }
    }
}