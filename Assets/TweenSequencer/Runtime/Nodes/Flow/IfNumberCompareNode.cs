using System.Collections;
using System.Collections.Generic;
using XNode;

namespace TweenSequencer.Runtime {
    public enum NumberCompareOperator {
        Equal,
        NotEqual,
        Greater,
        GreaterOrEqual,
        Less,
        LessOrEqual
    }

    [CreateNodeMenu("TweenSequencer/Flow/If Number Compare")]
    public class IfNumberCompareNode : TweenScenarioNode {
        [Input(backingValue = ShowBackingValue.Never)] public bool input;
        [Output(backingValue = ShowBackingValue.Never)] public bool onTrue;
        [Output(backingValue = ShowBackingValue.Never)] public bool onFalse;

        public string leftParameterKey;
        public float leftValue;
        public string rightParameterKey;
        public float rightValue;
        public NumberCompareOperator compareOperator = NumberCompareOperator.Equal;

        private bool _lastResult;

        public override IEnumerator Execute(TweenScenarioContext context) {
            var left = string.IsNullOrWhiteSpace(leftParameterKey) ? leftValue : context.GetParameter(leftParameterKey, leftValue);
            var right = string.IsNullOrWhiteSpace(rightParameterKey) ? rightValue : context.GetParameter(rightParameterKey, rightValue);

            switch (compareOperator) {
                case NumberCompareOperator.Equal: _lastResult = left == right; break;
                case NumberCompareOperator.NotEqual: _lastResult = left != right; break;
                case NumberCompareOperator.Greater: _lastResult = left > right; break;
                case NumberCompareOperator.GreaterOrEqual: _lastResult = left >= right; break;
                case NumberCompareOperator.Less: _lastResult = left < right; break;
                case NumberCompareOperator.LessOrEqual: _lastResult = left <= right; break;
                default: _lastResult = false; break;
            }

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
