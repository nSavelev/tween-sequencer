using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace TweenSequencer.Runtime
{
    [CreateNodeMenu("TweenSequencer/Transform/DOLocalRotate")]
    public class TransformDOLocalRotateNode : TransformTweenNodeBase
    {
        public Vector3 to;
        public string toParameterKey;
        public RotateMode mode = RotateMode.Fast;

        public override IEnumerator Execute(TweenScenarioContext c)
        {
            var t = GetTarget(c);
            if (t == null) yield break;
            var tw = t.DOLocalRotate(ResolveVector3(c, toParameterKey, to, false, true), duration, mode);
            Apply(tw);
            yield return TweenAwaiter.Wait(tw);
        }
    }
}