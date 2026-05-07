using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace TweenSequencer.Runtime
{
    [CreateNodeMenu("TweenSequencer/Invoke/Method")]
    public class InvokeMethodNode : TweenScenarioNode
    {
        [Input(backingValue = ShowBackingValue.Never)]
        public bool input;

        [Output(backingValue = ShowBackingValue.Never)]
        public bool next;

        public string targetParameterKey;
        public string methodName;
        public bool waitForCompletion = true;
        public List<string> parameterKeys = new();

        public override IEnumerator Execute(TweenScenarioContext context)
        {
            var target = context.GetParameter<Component>(targetParameterKey);
            if (target == null || string.IsNullOrWhiteSpace(methodName)) yield break;
            var args = new List<object>(parameterKeys.Count);
            for (var i = 0; i < parameterKeys.Count; i++)
            {
                context.TryGetParameter(parameterKeys[i], out var value);
                args.Add(value);
            }

            var methods = target.GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            MethodInfo selected = null;
            for (var i = 0; i < methods.Length; i++)
            {
                var m = methods[i];
                if (m.Name == methodName && m.GetParameters().Length == args.Count)
                {
                    selected = m;
                    break;
                }
            }

            if (selected == null) yield break;
            var parameters = selected.GetParameters();
            var convertedArgs = new object[parameters.Length];
            for (var i = 0; i < parameters.Length; i++)
                convertedArgs[i] = ConvertArgument(args[i], parameters[i].ParameterType);
            var result = selected.Invoke(target, convertedArgs);
            if (!waitForCompletion || result == null) yield break;
            if (result is IEnumerator enumerator)
            {
                yield return enumerator;
                yield break;
            }

            if (result is YieldInstruction || result is CustomYieldInstruction || result is AsyncOperation)
                yield return result;
        }

        private static object ConvertArgument(object value, Type targetType)
        {
            if (value == null) return targetType.IsValueType ? Activator.CreateInstance(targetType) : null;
            if (targetType.IsInstanceOfType(value)) return value;
            try
            {
                return Convert.ChangeType(value, targetType);
            }
            catch
            {
                return targetType.IsValueType ? Activator.CreateInstance(targetType) : null;
            }
        }
    }
}