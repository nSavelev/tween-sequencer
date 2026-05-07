using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace TweenSequencer.Runtime
{
    [CreateNodeMenu("TweenSequencer/Transform/DOLookAt")]
    public class TransformDOLookAtNode : TransformTweenNodeBase
    {
        public Vector3 toward;
        public string towardParameterKey;
        public AxisConstraint axisConstraint = AxisConstraint.None;
        public Vector3 up = Vector3.up;

        public override IEnumerator Execute(TweenScenarioContext c)
        {
            var t = GetTarget(c);
            if (t == null) yield break;
            var tw = t.DOLookAt(ResolveVector3(c, towardParameterKey, toward, true, false), duration, axisConstraint,
                up);
            Apply(tw);
            yield return TweenAwaiter.Wait(tw);
        }
    }
}