using System.Collections;
using DG.Tweening;

namespace TweenSequencer.Runtime
{
    [CreateNodeMenu("TweenSequencer/TextMeshProUGUI/DOLineSpacing")]
    public class TMPUGUIDOLineSpacingNode : TextMeshProUGUITweenNodeBase
    {
        public float to;
        public string toParameterKey;

        public override IEnumerator Execute(TweenScenarioContext c)
        {
            var t = GetTarget(c);
            if (t == null) yield break;
            var tw = DOTween.To(() => t.lineSpacing, x => t.lineSpacing = x, ResolveFloat(c, toParameterKey, to),
                duration);
            Apply(tw);
            yield return TweenAwaiter.Wait(tw);
        }
    }
}