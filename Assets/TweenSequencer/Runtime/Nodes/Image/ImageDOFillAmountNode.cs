using System.Collections;
using DG.Tweening;

namespace TweenSequencer.Runtime
{
    [CreateNodeMenu("TweenSequencer/Image/DOFillAmount")]
    public class ImageDOFillAmountNode : ImageTweenNodeBase
    {
        public float to = 1f;

        [ParameterKey]
        public string toParameterKey;

        public override IEnumerator Execute(TweenScenarioContext context)
        {
            var target = GetTarget(context);
            if (target == null) yield break;

            var toValue = ResolveToValue(context);
            var tween = target.DOFillAmount(toValue, duration);
            Apply(tween);
            yield return TweenAwaiter.Wait(tween);
        }

        private float ResolveToValue(TweenScenarioContext context)
        {
            if (string.IsNullOrWhiteSpace(toParameterKey)) return to;

            return context.GetParameter(toParameterKey, to);
        }
    }
}