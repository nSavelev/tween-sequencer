using UnityEngine;
using UnityEngine.UI;

namespace TweenSequencer.Runtime
{
    public abstract class ImageTweenNodeBase : TweenOperationNode
    {
        [Input(backingValue = ShowBackingValue.Never)]
        public bool input;

        [Output(backingValue = ShowBackingValue.Never)]
        public bool next;

        public string targetParameterKey;

        protected Image GetTarget(TweenScenarioContext context)
        {
            return context.GetParameter<Image>(targetParameterKey);
        }

        protected float ResolveFloat(TweenScenarioContext context, string parameterKey, float fallback)
        {
            if (string.IsNullOrWhiteSpace(parameterKey)) return fallback;
            return context.GetParameter(parameterKey, fallback);
        }

        protected Color ResolveColor(TweenScenarioContext context, string parameterKey, Color fallback)
        {
            if (string.IsNullOrWhiteSpace(parameterKey)) return fallback;
            return context.GetParameter(parameterKey, fallback);
        }
    }
}