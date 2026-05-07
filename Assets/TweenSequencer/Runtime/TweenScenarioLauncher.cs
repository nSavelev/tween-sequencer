using System.Collections.Generic;
using UnityEngine;

namespace TweenSequencer.Runtime
{
    public class TweenScenarioLauncher : MonoBehaviour
    {
        [SerializeField]
        private TweenScenarioRunner runner;

        [SerializeField]
        private List<NamedParameter> parameters = new();

        public void Play()
        {
            PlayCoroutine();
        }

        public Coroutine PlayCoroutine()
        {
            if (runner == null)
            {
                Debug.LogWarning("TweenScenarioLauncher: runner is not assigned", this);
                return null;
            }

            return runner.PlayCoroutine(parameters);
        }

        public Coroutine PlayCoroutine(params NamedParameter[] runtimeParameters)
        {
            if (runner == null)
            {
                Debug.LogWarning("TweenScenarioLauncher: runner is not assigned", this);
                return null;
            }

            return runner.PlayCoroutine(runtimeParameters);
        }

        [ContextMenu("Play Scenario")]
        private void PlayFromContextMenu()
        {
            Play();
        }
    }
}