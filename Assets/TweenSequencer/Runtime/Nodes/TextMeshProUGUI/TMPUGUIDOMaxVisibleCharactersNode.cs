using System.Collections;
using DG.Tweening;

namespace TweenSequencer.Runtime
{
    [CreateNodeMenu("TweenSequencer/TextMeshProUGUI/DOMaxVisibleCharacters")]
    public class TMPUGUIDOMaxVisibleCharactersNode : TextMeshProUGUITweenNodeBase
    {
        public int to = 10;
        public string toParameterKey;

        public override IEnumerator Execute(TweenScenarioContext c)
        {
            var t = GetTarget(c);
            if (t == null) yield break;
            var tw = DOTween.To(() => t.maxVisibleCharacters, x => t.maxVisibleCharacters = x,
                ResolveInt(c, toParameterKey, to), duration);
            Apply(tw);
            yield return TweenAwaiter.Wait(tw);
        }
    }
}