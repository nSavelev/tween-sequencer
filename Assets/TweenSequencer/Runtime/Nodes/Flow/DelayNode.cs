using System.Collections;
using UnityEngine;

namespace TweenSequencer.Runtime
{
    [CreateNodeMenu("TweenSequencer/Flow/Delay")]
    public class DelayNode : TweenScenarioNode
    {
        [Input(backingValue = ShowBackingValue.Never)]
        public bool input;

        [Output(backingValue = ShowBackingValue.Never)]
        public bool next;

        public float seconds = 0.25f;

        public override IEnumerator Execute(TweenScenarioContext context)
        {
            if (seconds > 0f) yield return new WaitForSeconds(seconds);
        }
    }
}