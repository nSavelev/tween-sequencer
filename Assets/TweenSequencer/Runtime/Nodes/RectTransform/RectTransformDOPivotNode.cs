using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace TweenSequencer.Runtime
{
    [CreateNodeMenu("TweenSequencer/RectTransform/DOPivot")]
    public class RectTransformDOPivotNode : RectTransformTweenNodeBase
    {
        public Vector2 to;
        public string toParameterKey;

        public override IEnumerator Execute(TweenScenarioContext c)
        {
            var t = GetTarget(c);
            if (t == null) yield break;
            var tw = t.DOPivot(ResolveVector2(c, toParameterKey, to), duration);
            Apply(tw);
            yield return TweenAwaiter.Wait(tw);
        }
    }
}