using System;
using System.Collections;

namespace TweenSequencer.Runtime
{
    [CreateNodeMenu("TweenSequencer/Invoke/Parameter")]
    public class InvokeParameterNode : TweenScenarioNode
    {
        [Input(backingValue = ShowBackingValue.Never)]
        public bool input;

        [Output(backingValue = ShowBackingValue.Never)]
        public bool next;

        public string parameterKey;
        public bool waitForCompletion = true;

        public override IEnumerator Execute(TweenScenarioContext context)
        {
            if (string.IsNullOrWhiteSpace(parameterKey) || !context.TryGetParameter(parameterKey, out var value) ||
                value == null) yield break;

            if (value is Action action)
            {
                action.Invoke();
                yield break;
            }

            if (value is Func<IEnumerator> coroutineFactory)
            {
                if (!waitForCompletion)
                {
                    var routineAsync = coroutineFactory.Invoke();
                    if (routineAsync != null && context.Host != null) context.Host.StartCoroutine(routineAsync);
                    yield break;
                }

                var routine = coroutineFactory.Invoke();
                if (routine != null) yield return routine;
            }
        }
    }
}