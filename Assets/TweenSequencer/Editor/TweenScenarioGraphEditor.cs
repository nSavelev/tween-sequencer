using TweenSequencer.Runtime;
using UnityEditor;

// Editor integration: allow assigning a Runner from the Graph editing window

namespace TweenSequencer.Editor
{
    [CustomEditor(typeof(TweenScenarioGraph))]
    public class TweenScenarioGraphEditor : UnityEditor.Editor
    {
        private TweenScenarioGraph _activeGraph;

        private void OnEnable()
        {
            Selection.selectionChanged += OnSelectionChanged;
            OnSelectionChanged();
        }

        private void OnDisable()
        {
            Selection.selectionChanged -= OnSelectionChanged;
        }

        private void OnSelectionChanged()
        {
            if (Selection.activeObject is TweenScenarioGraph g) _activeGraph = g;
            else _activeGraph = null;
        }

        public override void OnInspectorGUI()
        {
            var graph = _activeGraph ?? (TweenScenarioGraph)target;
            if (graph == null) return;

            // Show default graph properties
            serializedObject.Update();
            DrawDefaultInspector();
            EditorGUILayout.Space();

            // // Runner assignment panel in graph editor window
            // graph.runner = (TweenScenarioRunner)EditorGUILayout.ObjectField("Runner", graph.runner, typeof(TweenScenarioRunner), true);
            // // Simple help about dropdowns when Runner is assigned
            // if (graph.runner != null) {
            //     EditorGUILayout.HelpBox("Runner assigned. ParameterKey fields in nodes will be displayed as dropdowns when using ParameterKey attribute.", MessageType.Info);
            //     // Optional: show dropdowns diagnostics here later
            // }

            serializedObject.ApplyModifiedProperties();
        }
    }
}