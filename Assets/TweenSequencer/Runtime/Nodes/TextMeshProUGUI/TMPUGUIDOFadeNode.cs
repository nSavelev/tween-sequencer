using System.Collections;
using DG.Tweening;

namespace TweenSequencer.Runtime
{
    [CreateNodeMenu("TweenSequencer/TextMeshProUGUI/DOFade")]
    public class TMPUGUIDOFadeNode : TextMeshProUGUITweenNodeBase
    {
        public float to = 1f;
        public string toParameterKey;

        public override IEnumerator Execute(TweenScenarioContext c)
        {
            var t = GetTarget(c);
            if (t == null) yield break;
            var tw = t.DOFade(ResolveFloat(c, toParameterKey, to), duration);
            Apply(tw);
            yield return TweenAwaiter.Wait(tw);
        }
    }
}