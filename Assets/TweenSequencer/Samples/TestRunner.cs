using System.Collections;
using System.Collections.Generic;
using TweenSequencer.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class TestRunner : MonoBehaviour
{
    [SerializeField]
    private TweenScenarioRunner _runner;

    [SerializeField]
    private Image _image;

    public void Run()
    {
        var parameters = new List<NamedParameter>
        {
            new() { key = "Image", type = NamedParameterType.Object, objectValue = _image.gameObject },
            new() { key = "TargetFill", type = NamedParameterType.Float, floatValue = 0f },
            NamedParameter.FromCoroutineFactory("await", () => OnAwait())
        };
        _runner.PlayCoroutine(parameters);
    }

    private IEnumerator OnAwait()
    {
        Debug.Log("AWAIT! start");
        yield return new WaitForSeconds(2);
        Debug.Log("AWAIT! end");
    }
}