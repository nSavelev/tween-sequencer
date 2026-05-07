using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace TweenSequencer.Runtime
{
    [CreateNodeMenu("TweenSequencer/Transform/DOPunchScale")]
    public class TransformDOPunchScaleNode : TransformTweenNodeBase
    {
        public Vector3 punch;
        public int vibrato = 10;
        public float elasticity = 1f;

        public override IEnumerator Execute(TweenScenarioContext c)
        {
            var t = GetTarget(c);
            if (t == null) yield break;
            var tw = t.DOPunchScale(punch, duration, vibrato, elasticity);
            Apply(tw);
            yield return TweenAwaiter.Wait(tw);
        }
    }
}