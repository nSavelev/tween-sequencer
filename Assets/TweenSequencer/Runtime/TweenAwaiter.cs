using System.Collections;
using DG.Tweening;

namespace TweenSequencer.Runtime
{
    public static class TweenAwaiter
    {
        public static IEnumerator Wait(Tween tween)
        {
            if (tween == null || !tween.active) yield break;

            yield return tween.WaitForCompletion();
        }
    }
}