using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace TweenSequencer.Runtime
{
    [CreateNodeMenu("TweenSequencer/Transform/DOShakeScale")]
    public class TransformDOShakeScaleNode : TransformTweenNodeBase
    {
        public Vector3 strength = Vector3.one;
        public int vibrato = 10;
        public float randomness = 90f;
        public bool fadeOut = true;

        public override IEnumerator Execute(TweenScenarioContext c)
        {
            var t = GetTarget(c);
            if (t == null) yield break;
            var tw = t.DOShakeScale(duration, strength, vibrato, randomness, fadeOut);
            Apply(tw);
            yield return TweenAwaiter.Wait(tw);
        }
    }
}