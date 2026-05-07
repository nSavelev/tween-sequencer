using System.Collections;
using System.Collections.Generic;

namespace TweenSequencer.Runtime
{
    [CreateNodeMenu("TweenSequencer/Flow/Parallel")]
    public class ParallelNode : TweenScenarioNode
    {
        [Input(backingValue = ShowBackingValue.Never)]
        public bool input;

        [Output(backingValue = ShowBackingValue.Never)]
        public bool branchA;

        [Output(backingValue = ShowBackingValue.Never)]
        public bool branchB;

        [Output(backingValue = ShowBackingValue.Never)]
        public bool branchC;

        [Output(backingValue = ShowBackingValue.Never)]
        public bool next;

        public override IEnumerator Execute(TweenScenarioContext context)
        {
            yield break;
        }

        public override IEnumerable<TweenScenarioNode> GetNextNodes()
        {
            yield break;
        }

        public IEnumerable<TweenScenarioNode> GetBranchNodes()
        {
            return GetConnected("branchA", "branchB", "branchC");
        }

        public IEnumerable<TweenScenarioNode> GetAfterNodes()
        {
            return GetConnected("next");
        }

        private IEnumerable<TweenScenarioNode> GetConnected(params string[] ports)
        {
            for (var p = 0; p < ports.Length; p++)
            {
                var port = GetOutputPort(ports[p]);
                if (port == null || !port.IsConnected) continue;
                for (var i = 0; i < port.ConnectionCount; i++)
                {
                    var node = port.GetConnection(i).node as TweenScenarioNode;
                    if (node != null) yield return node;
                }
            }
        }
    }
}