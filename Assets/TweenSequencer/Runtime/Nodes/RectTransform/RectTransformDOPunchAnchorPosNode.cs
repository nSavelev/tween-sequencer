using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace TweenSequencer.Runtime
{
    [CreateNodeMenu("TweenSequencer/RectTransform/DOPunchAnchorPos")]
    public class RectTransformDOPunchAnchorPosNode : RectTransformTweenNodeBase
    {
        public Vector2 punch;
        public int vibrato = 10;
        public float elasticity = 1f;
        public bool snapping;

        public override IEnumerator Execute(TweenScenarioContext c)
        {
            var t = GetTarget(c);
            if (t == null) yield break;
            var tw = t.DOPunchAnchorPos(punch, duration, vibrato, elasticity, snapping);
            Apply(tw);
            yield return TweenAwaiter.Wait(tw);
        }
    }
}