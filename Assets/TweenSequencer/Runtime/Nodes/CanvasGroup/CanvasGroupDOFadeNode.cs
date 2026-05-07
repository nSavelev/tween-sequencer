using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace TweenSequencer.Runtime
{
    [CreateNodeMenu("TweenSequencer/CanvasGroup/DOFade")]
    public class CanvasGroupDOFadeNode : TweenOperationNode
    {
        [Input(backingValue = ShowBackingValue.Never)]
        public bool input;

        [Output(backingValue = ShowBackingValue.Never)]
        public bool next;

        public string targetParameterKey;
        public float to = 1f;
        public string toParameterKey;

        public override IEnumerator Execute(TweenScenarioContext context)
        {
            var target = context.GetParameter<CanvasGroup>(targetParameterKey);
            if (target == null) yield break;
            var tween = target.DOFade(
                string.IsNullOrWhiteSpace(toParameterKey) ? to : context.GetParameter(toParameterKey, to), duration);
            Apply(tween);
            yield return TweenAwaiter.Wait(tween);
        }
    }
}