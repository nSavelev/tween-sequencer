using System;
using System.Collections;
using System.Collections.Generic;
using XNode;

namespace TweenSequencer.Runtime
{
    [Serializable]
    public abstract class TweenScenarioNode : Node
    {
        public abstract IEnumerator Execute(TweenScenarioContext context);

        public virtual IEnumerable<TweenScenarioNode> GetNextNodes()
        {
            var output = GetOutputPort("next");
            if (output == null || !output.IsConnected) yield break;

            for (var i = 0; i < output.ConnectionCount; i++)
            {
                var node = output.GetConnection(i).node as TweenScenarioNode;
                if (node != null) yield return node;
            }
        }
    }
}