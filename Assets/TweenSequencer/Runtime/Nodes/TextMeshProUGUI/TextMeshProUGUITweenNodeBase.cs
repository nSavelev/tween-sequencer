using TMPro;
using UnityEngine;

namespace TweenSequencer.Runtime
{
    public abstract class TextMeshProUGUITweenNodeBase : TweenOperationNode
    {
        [Input(backingValue = ShowBackingValue.Never)]
        public bool input;

        [Output(backingValue = ShowBackingValue.Never)]
        public bool next;

        public string targetParameterKey;

        protected TextMeshProUGUI GetTarget(TweenScenarioContext context)
        {
            return context.GetParameter<TextMeshProUGUI>(targetParameterKey);
        }

        protected float ResolveFloat(TweenScenarioContext context, string parameterKey, float fallback)
        {
            return string.IsNullOrWhiteSpace(parameterKey) ? fallback : context.GetParameter(parameterKey, fallback);
        }

        protected int ResolveInt(TweenScenarioContext context, string parameterKey, int fallback)
        {
            return string.IsNullOrWhiteSpace(parameterKey) ? fallback : context.GetParameter(parameterKey, fallback);
        }

        protected Color ResolveColor(TweenScenarioContext context, string parameterKey, Color fallback)
        {
            return string.IsNullOrWhiteSpace(parameterKey) ? fallback : context.GetParameter(parameterKey, fallback);
        }
    }
}