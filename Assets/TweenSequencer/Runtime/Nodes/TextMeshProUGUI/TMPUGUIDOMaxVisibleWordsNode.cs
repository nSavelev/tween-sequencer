using System.Collections;
using DG.Tweening;

namespace TweenSequencer.Runtime
{
    [CreateNodeMenu("TweenSequencer/TextMeshProUGUI/DOMaxVisibleWords")]
    public class TMPUGUIDOMaxVisibleWordsNode : TextMeshProUGUITweenNodeBase
    {
        public int to = 10;
        public string toParameterKey;

        public override IEnumerator Execute(TweenScenarioContext c)
        {
            var t = GetTarget(c);
            if (t == null) yield break;
            var tw = DOTween.To(() => t.maxVisibleWords, x => t.maxVisibleWords = x, ResolveInt(c, toParameterKey, to),
                duration);
            Apply(tw);
            yield return TweenAwaiter.Wait(tw);
        }
    }
}