#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TweenSequencer.Runtime;
using UnityEngine;

namespace TweenSequencer.Editor
{
    public static class TweenScenarioEditorUtils
    {
        public static List<string> GetAllAvailableParameterKeys()
        {
            var keys = new List<string>();
            var runners = Resources.FindObjectsOfTypeAll<TweenScenarioRunner>();
            foreach (var r in runners)
            {
                var f = typeof(TweenScenarioRunner).GetField("initialParameters",
                    BindingFlags.NonPublic | BindingFlags.Instance);
                if (f != null)
                {
                    var list = f.GetValue(r) as List<NamedParameter>;
                    if (list != null)
                        foreach (var p in list)
                            if (p != null && !string.IsNullOrWhiteSpace(p.key))
                                keys.Add(p.key);
                }
            }

            return keys.Distinct().ToList();
        }
    }
}
#endif