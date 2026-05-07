using System.Collections;
using DG.Tweening;

namespace TweenSequencer.Runtime
{
    [CreateNodeMenu("TweenSequencer/Transform/DOScaleY")]
    public class TransformDOScaleYNode : TransformTweenNodeBase
    {
        public float to = 1f;
        public string toParameterKey;

        public override IEnumerator Execute(TweenScenarioContext c)
        {
            var t = GetTarget(c);
            if (t == null) yield break;
            var tw = t.DOScaleY(ResolveFloat(c, toParameterKey, to), duration);
            Apply(tw);
            yield return TweenAwaiter.Wait(tw);
        }
    }
}