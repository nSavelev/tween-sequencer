using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace TweenSequencer.Runtime
{
    [CreateNodeMenu("TweenSequencer/TextMeshProUGUI/DOFaceColor")]
    public class TMPUGUIDOFaceColorNode : TextMeshProUGUITweenNodeBase
    {
        public Color to = Color.white;
        public string toParameterKey;

        public override IEnumerator Execute(TweenScenarioContext c)
        {
            var t = GetTarget(c);
            if (t == null) yield break;
            var toColor = ResolveColor(c, toParameterKey, to);
            var from = (Color)t.faceColor;
            var tw = DOTween.To(() => 0f, x => t.faceColor = Color.Lerp(from, toColor, x), 1f, duration);
            Apply(tw);
            yield return TweenAwaiter.Wait(tw);
        }
    }
}