using System.Collections;
using DG.Tweening;

namespace TweenSequencer.Runtime
{
    [CreateNodeMenu("TweenSequencer/RectTransform/DOPivotX")]
    public class RectTransformDOPivotXNode : RectTransformTweenNodeBase
    {
        public float to;
        public string toParameterKey;

        public override IEnumerator Execute(TweenScenarioContext c)
        {
            var t = GetTarget(c);
            if (t == null) yield break;
            var tw = t.DOPivotX(ResolveFloat(c, toParameterKey, to), duration);
            Apply(tw);
            yield return TweenAwaiter.Wait(tw);
        }
    }
}