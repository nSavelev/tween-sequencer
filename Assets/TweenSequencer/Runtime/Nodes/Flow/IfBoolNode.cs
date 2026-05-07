using System.Collections;
using System.Collections.Generic;
using XNode;

namespace TweenSequencer.Runtime {
    [CreateNodeMenu("TweenSequencer/Flow/If Bool")]
    public class IfBoolNode : TweenScenarioNode {
        [Input(backingValue = ShowBackingValue.Never)] public bool input;
        [Output(backingValue = ShowBackingValue.Never)] public bool onTrue;
        [Output(backingValue = ShowBackingValue.Never)] public bool onFalse;

        public string boolParameterKey;
        public bool value;

        private bool _lastResult;

        public override IEnumerator Execute(TweenScenarioContext context) {
            _lastResult = !string.IsNullOrEmpty(boolParameterKey)
                ? context.GetParameter(boolParameterKey, value)
                : value;
            yield break;
        }

        public override IEnumerable<TweenScenarioNode> GetNextNodes() {
            var output = GetOutputPort(_lastResult ? "onTrue" : "onFalse");
            if (output == null || !output.IsConnected) {
                yield break;
            }

            for (int i = 0; i < output.ConnectionCount; i++) {
                var node = output.GetConnection(i).node as TweenScenarioNode;
                if (node != null) {
                    yield return node;
                }
            }
        }
    }
}
