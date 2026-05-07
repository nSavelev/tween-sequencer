using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace TweenSequencer.Runtime
{
    [CreateNodeMenu("TweenSequencer/Image/DOColor")]
    public class ImageDOColorNode : ImageTweenNodeBase
    {
        public Color to = Color.white;
        public string toParameterKey;

        public override IEnumerator Execute(TweenScenarioContext context)
        {
            var target = GetTarget(context);
            if (target == null) yield break;
            var tween = target.DOColor(ResolveColor(context, toParameterKey, to), duration);
            Apply(tween);
            yield return TweenAwaiter.Wait(tween);
        }
    }
}