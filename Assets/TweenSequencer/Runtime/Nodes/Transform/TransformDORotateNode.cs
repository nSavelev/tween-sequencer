using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace TweenSequencer.Runtime
{
    [CreateNodeMenu("TweenSequencer/Transform/DORotate")]
    public class TransformDORotateNode : TransformTweenNodeBase
    {
        public Vector3 to;
        public string toParameterKey;
        public RotateMode mode = RotateMode.Fast;

        public override IEnumerator Execute(TweenScenarioContext c)
        {
            var t = GetTarget(c);
            if (t == null) yield break;

            var toValue = ResolveToValue(c);
            var tw = t.DORotate(toValue, duration, mode);
            Apply(tw);
            yield return TweenAwaiter.Wait(tw);
        }

        private Vector3 ResolveToValue(TweenScenarioContext context)
        {
            if (string.IsNullOrWhiteSpace(toParameterKey)) return to;

            if (context.TryGetParameter(toParameterKey, out var value))
            {
                if (value is Transform transformTarget) return transformTarget.rotation.eulerAngles;

                if (value is Vector3 vector) return vector;
            }

            return to;
        }
    }
}