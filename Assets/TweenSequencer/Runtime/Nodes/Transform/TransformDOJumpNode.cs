using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace TweenSequencer.Runtime
{
    [CreateNodeMenu("TweenSequencer/Transform/DOJump")]
    public class TransformDOJumpNode : TransformTweenNodeBase
    {
        public Vector3 endValue;
        public string endValueParameterKey;
        public float jumpPower = 1f;
        public string jumpPowerParameterKey;
        public int numJumps = 1;
        public bool snapping;

        public override IEnumerator Execute(TweenScenarioContext c)
        {
            var t = GetTarget(c);
            if (t == null) yield break;
            var tw = t.DOJump(ResolveVector3(c, endValueParameterKey, endValue, true, false),
                ResolveFloat(c, jumpPowerParameterKey, jumpPower), numJumps, duration, snapping);
            Apply(tw);
            yield return TweenAwaiter.Wait(tw);
        }
    }
}