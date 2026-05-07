using System.Collections;
using System.Collections.Generic;
using XNode;

namespace TweenSequencer.Runtime {
    public enum StringCompareOperator {
        Equal,
        NotEqual,
        Contains,
        StartsWith,
        EndsWith
    }

    [CreateNodeMenu("TweenSequencer/Flow/If String Compare")]
    public class IfStringCompareNode : TweenScenarioNode {
        [Input(backingValue = ShowBackingValue.Never)] public bool input;
        [Output(backingValue = ShowBackingValue.Never)] public bool onTrue;
        [Output(backingValue = ShowBackingValue.Never)] public bool onFalse;

        public string leftParameterKey;
        public string leftValue;
        public string rightParameterKey;
        public string rightValue;
        public StringCompareOperator compareOperator = StringCompareOperator.Equal;
        public bool ignoreCase;

        private bool _lastResult;

        public override IEnumerator Execute(TweenScenarioContext context) {
            var left = string.IsNullOrWhiteSpace(leftParameterKey) ? leftValue : context.GetParameter(leftParameterKey, leftValue);
            var right = string.IsNullOrWhiteSpace(rightParameterKey) ? rightValue : context.GetParameter(rightParameterKey, rightValue);

            left = left ?? string.Empty;
            right = right ?? string.Empty;

            var comparison = ignoreCase ? System.StringComparison.OrdinalIgnoreCase : System.StringComparison.Ordinal;
            switch (compareOperator) {
                case StringCompareOperator.Equal: _lastResult = string.Equals(left, right, comparison); break;
                case StringCompareOperator.NotEqual: _lastResult = !string.Equals(left, right, comparison); break;
                case StringCompareOperator.Contains: _lastResult = left.IndexOf(right, comparison) >= 0; break;
                case StringCompareOperator.StartsWith: _lastResult = left.StartsWith(right, comparison); break;
                case StringCompareOperator.EndsWith: _lastResult = left.EndsWith(right, comparison); break;
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
