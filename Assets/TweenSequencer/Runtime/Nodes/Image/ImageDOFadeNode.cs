using System.Collections;
using DG.Tweening;

namespace TweenSequencer.Runtime
{
    [CreateNodeMenu("TweenSequencer/Image/DOFade")]
    public class ImageDOFadeNode : ImageTweenNodeBase
    {
        public float to = 1f;
        public string toParameterKey;

        public override IEnumerator Execute(TweenScenarioContext context)
        {
            var target = GetTarget(context);
            if (target == null) yield break;
            var tween = target.DOFade(ResolveFloat(context, toParameterKey, to), duration);
            Apply(tween);
            yield return TweenAwaiter.Wait(tween);
        }
    }
}