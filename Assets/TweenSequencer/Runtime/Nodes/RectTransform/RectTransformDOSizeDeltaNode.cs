using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace TweenSequencer.Runtime
{
    [CreateNodeMenu("TweenSequencer/RectTransform/DOSizeDelta")]
    public class RectTransformDOSizeDeltaNode : RectTransformTweenNodeBase
    {
        public Vector2 to;
        public string toParameterKey;
        public bool snapping;

        public override IEnumerator Execute(TweenScenarioContext c)
        {
            var t = GetTarget(c);
            if (t == null) yield break;
            var tw = t.DOSizeDelta(ResolveVector2(c, toParameterKey, to), duration, snapping);
            Apply(tw);
            yield return TweenAwaiter.Wait(tw);
        }
    }
}