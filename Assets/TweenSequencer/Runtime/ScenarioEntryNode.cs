using System;
using System.Collections;

namespace TweenSequencer.Runtime
{
    [CreateNodeMenu("TweenSequencer/Entry")]
    [Serializable]
    public class ScenarioEntryNode : TweenScenarioNode
    {
        [Output(backingValue = ShowBackingValue.Never)]
        public bool next;

        public override IEnumerator Execute(TweenScenarioContext context)
        {
            yield break;
        }
    }
}