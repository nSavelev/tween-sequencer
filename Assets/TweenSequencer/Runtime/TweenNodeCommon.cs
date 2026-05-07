using DG.Tweening;

namespace TweenSequencer.Runtime
{
    public abstract class TweenOperationNode : TweenScenarioNode
    {
        public float duration = 0.3f;
        public Ease ease = Ease.OutQuad;
        public int loops;
        public LoopType loopType = LoopType.Restart;
        public bool isRelative;

        protected void Apply(Tween tween)
        {
            if (tween == null) return;

            tween.SetEase(ease);
            if (isRelative) tween.SetRelative();

            if (loops != 0) tween.SetLoops(loops, loopType);
        }
    }
}