using TweenSequencer.Runtime;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace TweenSequencer.Editor
{
    public static class TweenSequencerEditorMenu
    {
        [MenuItem("Assets/Create/TweenSequencer/Scenario Graph", priority = 301)]
        public static void CreateGraphAsset()
        {
            var graph = ScriptableObject.CreateInstance<TweenScenarioGraph>();
            var path = AssetDatabase.GenerateUniqueAssetPath("Assets/NewTweenScenario.asset");
            AssetDatabase.CreateAsset(graph, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Selection.activeObject = graph;
            NodeEditorWindow.Open(graph);
        }

        [MenuItem("Tools/TweenSequencer/Open Selected Graph")]
        public static void OpenSelectedGraph()
        {
            if (Selection.activeObject is TweenScenarioGraph graph) NodeEditorWindow.Open(graph);
        }

        [MenuItem("Tools/TweenSequencer/Save Selected Graph Nodes")]
        public static void SaveSelectedGraphNodes()
        {
            if (!(Selection.activeObject is TweenScenarioGraph graph))
            {
                Debug.LogWarning("TweenScenario: select TweenScenarioGraph asset first");
                return;
            }

            var changed = TweenScenarioGraphPersistence.SaveGraphNodes(graph);
            AssetDatabase.SaveAssets();
            Debug.Log(changed
                ? "TweenScenario: nodes were reattached and saved"
                : "TweenScenario: nodes are already attached", graph);
        }

        [MenuItem("Tools/TweenSequencer/Repair And Refresh All Graphs")]
        public static void RepairAndRefreshAllGraphs()
        {
            var repaired = TweenScenarioGraphPersistence.RepairAllGraphs(true);
            Debug.Log($"TweenScenario: repaired {repaired} graph(s)");
        }

        [MenuItem("Tools/TweenSequencer/Refresh Parameter Keys Cache")]
        public static void RefreshParameterKeysCache()
        {
            // Trigger a refresh by touching all graphs; actual per-node mapping is handled by drawers at runtime
            Debug.Log("TweenSequencer: refresh request issued (UI-only)");
        }
    }
}