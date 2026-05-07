using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace TweenSequencer.Runtime
{
    [CreateNodeMenu("TweenSequencer/Transform/DOPunchPosition")]
    public class TransformDOPunchPositionNode : TransformTweenNodeBase
    {
        public Vector3 punch;
        public int vibrato = 10;
        public float elasticity = 1f;
        public bool snapping;

        public override IEnumerator Execute(TweenScenarioContext c)
        {
            var t = GetTarget(c);
            if (t == null) yield break;
            var tw = t.DOPunchPosition(punch, duration, vibrato, elasticity, snapping);
            Apply(tw);
            yield return TweenAwaiter.Wait(tw);
        }
    }
}