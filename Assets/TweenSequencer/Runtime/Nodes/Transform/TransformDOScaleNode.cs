using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace TweenSequencer.Runtime
{
    [CreateNodeMenu("TweenSequencer/Transform/DOScale")]
    public class TransformDOScaleNode : TransformTweenNodeBase
    {
        public Vector3 to = Vector3.one;
        public string toParameterKey;

        public override IEnumerator Execute(TweenScenarioContext c)
        {
            var t = GetTarget(c);
            if (t == null) yield break;
            var tw = t.DOScale(ResolveVector3(c, toParameterKey, to, false, false), duration);
            Apply(tw);
            yield return TweenAwaiter.Wait(tw);
        }
    }
}