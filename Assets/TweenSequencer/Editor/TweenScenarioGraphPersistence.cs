using System.Linq;
using TweenSequencer.Runtime;
using UnityEditor;
using UnityEngine;

namespace TweenSequencer.Editor
{
    public static class TweenScenarioGraphPersistence
    {
        public static bool SaveGraphNodes(TweenScenarioGraph graph)
        {
            if (graph == null) return false;

            var graphPath = AssetDatabase.GetAssetPath(graph);
            if (string.IsNullOrWhiteSpace(graphPath)) return false;

            var changed = false;
            var nodes = graph.nodes == null ? null : graph.nodes.ToArray();
            if (nodes == null) return false;

            for (var i = 0; i < nodes.Length; i++)
            {
                var node = nodes[i];
                if (node == null) continue;

                var nodePath = AssetDatabase.GetAssetPath(node);
                if (nodePath == graphPath) continue;

                if (!string.IsNullOrWhiteSpace(nodePath) && AssetDatabase.Contains(node)) continue;

                if (string.IsNullOrWhiteSpace(nodePath) || nodePath != graphPath)
                {
                    AssetDatabase.AddObjectToAsset(node, graph);
                    changed = true;
                }
            }

            if (changed) EditorUtility.SetDirty(graph);

            return changed;
        }

        public static void RefreshGraphAsset(TweenScenarioGraph graph)
        {
            if (graph == null) return;

            var graphPath = AssetDatabase.GetAssetPath(graph);
            if (string.IsNullOrWhiteSpace(graphPath)) return;

            AssetDatabase.ImportAsset(graphPath, ImportAssetOptions.ForceUpdate);
        }

        public static int RepairAllGraphs(bool forceRefresh)
        {
            var guids = AssetDatabase.FindAssets("t:TweenScenarioGraph");
            var repaired = 0;
            for (var i = 0; i < guids.Length; i++)
            {
                var path = AssetDatabase.GUIDToAssetPath(guids[i]);
                var graph = AssetDatabase.LoadAssetAtPath<TweenScenarioGraph>(path);
                if (graph == null) continue;

                if (SaveGraphNodes(graph)) repaired++;

                if (forceRefresh) RefreshGraphAsset(graph);
            }

            AssetDatabase.SaveAssets();
            return repaired;
        }
    }

    public class TweenScenarioGraphSaveProcessor : AssetModificationProcessor
    {
        private static string[] OnWillSaveAssets(string[] paths)
        {
            var graphs = Resources.FindObjectsOfTypeAll<TweenScenarioGraph>();
            for (var i = 0; i < graphs.Length; i++)
            {
                var path = AssetDatabase.GetAssetPath(graphs[i]);
                if (!string.IsNullOrWhiteSpace(path) && paths.Contains(path))
                    TweenScenarioGraphPersistence.SaveGraphNodes(graphs[i]);
            }

            return paths;
        }
    }

    [CustomEditor(typeof(TweenScenarioGraph))]
    public class TweenScenarioGraphInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            EditorGUILayout.Space();

            if (GUILayout.Button("Save Nodes To Asset"))
            {
                var graph = target as TweenScenarioGraph;
                var changed = TweenScenarioGraphPersistence.SaveGraphNodes(graph);
                AssetDatabase.SaveAssets();
                Debug.Log(changed
                    ? "TweenScenario: nodes were reattached and saved"
                    : "TweenScenario: nodes are already attached", graph);
            }

            if (GUILayout.Button("Refresh Graph Asset"))
            {
                var graph = target as TweenScenarioGraph;
                TweenScenarioGraphPersistence.RefreshGraphAsset(graph);
                Debug.Log("TweenScenario: graph asset refreshed", graph);
            }
        }
    }
}