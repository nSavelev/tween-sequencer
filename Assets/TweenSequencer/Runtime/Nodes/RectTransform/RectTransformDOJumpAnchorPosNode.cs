using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace TweenSequencer.Runtime
{
    [CreateNodeMenu("TweenSequencer/RectTransform/DOJumpAnchorPos")]
    public class RectTransformDOJumpAnchorPosNode : RectTransformTweenNodeBase
    {
        public Vector2 endValue;
        public float jumpPower = 1f;
        public int numJumps = 1;
        public bool snapping;

        public override IEnumerator Execute(TweenScenarioContext c)
        {
            var t = GetTarget(c);
            if (t == null) yield break;
            var tw = t.DOJumpAnchorPos(endValue, jumpPower, numJumps, duration, snapping);
            Apply(tw);
            yield return TweenAwaiter.Wait(tw);
        }
    }
}