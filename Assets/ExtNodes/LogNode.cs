using System.Collections;
using TweenSequencer.Runtime;
using UnityEngine;

[CreateNodeMenu("Debug/Log")]
public class LogNode : TweenScenarioNode
{
    [Input(backingValue = ShowBackingValue.Never)] public bool input;
    [Output(backingValue = ShowBackingValue.Never)] public bool next;
    public string message;
    
    public override IEnumerator Execute(TweenScenarioContext context)
    {
        Debug.Log(message);
        yield break;
    }
}
