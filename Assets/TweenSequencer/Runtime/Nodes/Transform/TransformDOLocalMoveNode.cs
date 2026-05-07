using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace TweenSequencer.Runtime
{
    [CreateNodeMenu("TweenSequencer/Transform/DOLocalMove")]
    public class TransformDOLocalMoveNode : TransformTweenNodeBase
    {
        public Vector3 to;
        public string toParameterKey;
        public bool snapping;

        public override IEnumerator Execute(TweenScenarioContext c)
        {
            var t = GetTarget(c);
            if (t == null) yield break;
            var tw = t.DOLocalMove(ResolveVector3(c, toParameterKey, to, true, false), duration, snapping);
            Apply(tw);
            yield return TweenAwaiter.Wait(tw);
        }
    }
}