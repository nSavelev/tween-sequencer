using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace TweenSequencer.Runtime
{
    [CreateNodeMenu("TweenSequencer/RectTransform/DOAnchorPos3D")]
    public class RectTransformDOAnchorPos3DNode : RectTransformTweenNodeBase
    {
        public Vector3 to;
        public string toParameterKey;
        public bool snapping;

        public override IEnumerator Execute(TweenScenarioContext c)
        {
            var t = GetTarget(c);
            if (t == null) yield break;
            var tw = t.DOAnchorPos3D(ResolveVector3(c, toParameterKey, to), duration, snapping);
            Apply(tw);
            yield return TweenAwaiter.Wait(tw);
        }
    }
}