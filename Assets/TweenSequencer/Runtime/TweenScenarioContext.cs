using System;
using System.Collections.Generic;
using UnityEngine;

namespace TweenSequencer.Runtime
{
    public class TweenScenarioContext
    {
        private readonly Dictionary<string, object> _parameters;
        public readonly MonoBehaviour Host;

        public TweenScenarioContext(MonoBehaviour host, IEnumerable<NamedParameter> overrides = null)
        {
            Host = host;
            _parameters = new Dictionary<string, object>(StringComparer.Ordinal);

            if (overrides == null) return;

            foreach (var item in overrides)
            {
                if (item == null || string.IsNullOrWhiteSpace(item.key)) continue;

                _parameters[item.key] = item.GetValue();
            }
        }

        public bool TryGetParameter(string key, out object value)
        {
            return _parameters.TryGetValue(key, out value);
        }

        public T GetParameter<T>(string key, T fallback = default)
        {
            if (!_parameters.TryGetValue(key, out var value) || value == null) return fallback;

            if (value is T cast) return cast;

            if (value is GameObject go) return go.GetComponent<T>();

            try
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch
            {
                return fallback;
            }
        }
    }
}