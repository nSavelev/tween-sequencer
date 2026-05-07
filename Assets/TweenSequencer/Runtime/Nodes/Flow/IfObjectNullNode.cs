using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace TweenSequencer.Runtime {
    [CreateNodeMenu("TweenSequencer/Flow/If Object Null")]
    public class IfObjectNullNode : TweenScenarioNode {
        [Input(backingValue = ShowBackingValue.Never)] public bool input;
        [Output(backingValue = ShowBackingValue.Never)] public bool onNull;
        [Output(backingValue = ShowBackingValue.Never)] public bool onNotNull;

        public string objectParameterKey;
        public Object fallbackObject;

        private bool _isNull;

        public override IEnumerator Execute(TweenScenarioContext context) {
            Object value;
            if (string.IsNullOrWhiteSpace(objectParameterKey)) {
                value = fallbackObject;
            } else {
                value = context.GetParameter<Object>(objectParameterKey, fallbackObject);
            }

            _isNull = value == null;
            yield break;
        }

        public override IEnumerable<TweenScenarioNode> GetNextNodes() {
            var output = GetOutputPort(_isNull ? "onNull" : "onNotNull");
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
