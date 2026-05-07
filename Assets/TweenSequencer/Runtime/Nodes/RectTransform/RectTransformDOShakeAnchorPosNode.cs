using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace TweenSequencer.Runtime
{
    [CreateNodeMenu("TweenSequencer/RectTransform/DOShakeAnchorPos")]
    public class RectTransformDOShakeAnchorPosNode : RectTransformTweenNodeBase
    {
        public Vector2 strength = Vector2.one;
        public int vibrato = 10;
        public float randomness = 90f;
        public bool snapping;
        public bool fadeOut = true;

        public override IEnumerator Execute(TweenScenarioContext c)
        {
            var t = GetTarget(c);
            if (t == null) yield break;
            var tw = t.DOShakeAnchorPos(duration, strength, vibrato, randomness, snapping, fadeOut);
            Apply(tw);
            yield return TweenAwaiter.Wait(tw);
        }
    }
}