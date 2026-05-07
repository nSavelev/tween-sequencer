using System.Collections;
using DG.Tweening;

namespace TweenSequencer.Runtime
{
    [CreateNodeMenu("TweenSequencer/Transform/DOLocalMoveZ")]
    public class TransformDOLocalMoveZNode : TransformTweenNodeBase
    {
        public float to;
        public string toParameterKey;
        public bool snapping;

        public override IEnumerator Execute(TweenScenarioContext c)
        {
            var t = GetTarget(c);
            if (t == null) yield break;
            var tw = t.DOLocalMoveZ(ResolveFloat(c, toParameterKey, to), duration, snapping);
            Apply(tw);
            yield return TweenAwaiter.Wait(tw);
        }
    }
}