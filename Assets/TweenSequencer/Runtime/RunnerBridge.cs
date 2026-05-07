using System;
using TweenSequencer.Runtime;

// Lightweight bridge to resolve a Runner for a Graph at runtime without embedding Runner into the Graph.
[Serializable]
public class RunnerBridge
{
    public TweenScenarioGraph graph;
    public TweenScenarioRunner runner;
}