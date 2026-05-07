using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace TweenSequencer.Runtime
{
    public class TweenScenarioRunner : MonoBehaviour
    {
        [SerializeField]
        private TweenScenarioGraph scenario;

        [SerializeField]
        private List<NamedParameter> initialParameters = new();

        private readonly Dictionary<Component, IComponentState> _initialStates = new();

        private bool _stateCaptured;

        // Expose for editor tooling: build dropdowns from existing keys
        public List<NamedParameter> InitialParameters => initialParameters;

        private void Awake()
        {
            CaptureInitialState();
        }

        public void Play()
        {
            PlayCoroutine();
        }

        public void ResetToInitialState()
        {
            if (!_stateCaptured) CaptureInitialState();

            foreach (var pair in _initialStates)
            {
                if (pair.Key == null) continue;

                DOTween.Kill(pair.Key);
                pair.Value.Restore(pair.Key);
            }
        }

        public Coroutine PlayCoroutine()
        {
            var context = new TweenScenarioContext(this, initialParameters);
            return StartCoroutine(PlayCoroutine(context));
        }

        public Coroutine PlayCoroutine(IEnumerable<NamedParameter> parameters)
        {
            var context = new TweenScenarioContext(this, MergeParameters(parameters));
            return StartCoroutine(PlayCoroutine(context));
        }

        public Coroutine PlayCoroutine(params NamedParameter[] parameters)
        {
            return PlayCoroutine((IEnumerable<NamedParameter>)parameters);
        }

        public IEnumerator PlayCoroutine(TweenScenarioContext context)
        {
            if (scenario == null)
            {
                Debug.LogWarning("TweenScenarioRunner: scenario is not assigned", this);
                yield break;
            }

            var entry = scenario.nodes.OfType<ScenarioEntryNode>().FirstOrDefault();
            if (entry == null)
            {
                Debug.LogWarning("TweenScenarioRunner: entry node not found", this);
                yield break;
            }

            yield return Traverse(entry, context);
        }

        private IEnumerator Traverse(TweenScenarioNode node, TweenScenarioContext context)
        {
            yield return node.Execute(context);

            if (node is ParallelNode parallelNode)
            {
                var pending = 0;
                foreach (var branch in parallelNode.GetBranchNodes())
                {
                    pending++;
                    StartCoroutine(RunBranch(branch, context, () => pending--));
                }

                while (pending > 0) yield return null;

                foreach (var nextNode in parallelNode.GetAfterNodes()) yield return Traverse(nextNode, context);

                yield break;
            }

            foreach (var next in node.GetNextNodes()) yield return Traverse(next, context);
        }

        private IEnumerator RunBranch(TweenScenarioNode branch, TweenScenarioContext context, Action onCompleted)
        {
            yield return Traverse(branch, context);
            onCompleted?.Invoke();
        }

        private IEnumerable<NamedParameter> MergeParameters(IEnumerable<NamedParameter> overrides)
        {
            var merged = new Dictionary<string, NamedParameter>(StringComparer.Ordinal);

            for (var i = 0; i < initialParameters.Count; i++)
            {
                var p = initialParameters[i];
                if (p == null || string.IsNullOrWhiteSpace(p.key)) continue;
                merged[p.key] = p;
            }

            if (overrides != null)
                foreach (var p in overrides)
                {
                    if (p == null || string.IsNullOrWhiteSpace(p.key)) continue;
                    merged[p.key] = p;
                }

            return merged.Values;
        }

        private void CaptureInitialState()
        {
            _initialStates.Clear();

            for (var i = 0; i < initialParameters.Count; i++)
            {
                var parameter = initialParameters[i];
                if (parameter == null || parameter.type != NamedParameterType.Object ||
                    parameter.objectValue == null) continue;

                var component = ResolveComponent(parameter.objectValue);
                if (component == null || _initialStates.ContainsKey(component)) continue;

                var snapshot = BuildSnapshot(component);
                if (snapshot != null) _initialStates.Add(component, snapshot);
            }

            _stateCaptured = true;
        }

        private static Component ResolveComponent(Object source)
        {
            if (source is Component component) return component;

            if (source is GameObject gameObject) return gameObject.transform;

            return null;
        }

        private static IComponentState BuildSnapshot(Component component)
        {
            if (component is RectTransform rectTransform) return new RectTransformState(rectTransform);
            if (component is Transform transform) return new TransformState(transform);
            if (component is Image image) return new ImageState(image);
            if (component is CanvasGroup canvasGroup) return new CanvasGroupState(canvasGroup);
            if (component is TextMeshProUGUI text) return new TextMeshProUGUIState(text);
            return null;
        }

        private interface IComponentState
        {
            void Restore(Component component);
        }

        private struct TransformState : IComponentState
        {
            private readonly Vector3 _position;
            private readonly Vector3 _localPosition;
            private readonly Quaternion _rotation;
            private readonly Quaternion _localRotation;
            private readonly Vector3 _localScale;

            public TransformState(Transform value)
            {
                _position = value.position;
                _localPosition = value.localPosition;
                _rotation = value.rotation;
                _localRotation = value.localRotation;
                _localScale = value.localScale;
            }

            public void Restore(Component component)
            {
                var value = component as Transform;
                if (value == null) return;
                value.position = _position;
                value.localPosition = _localPosition;
                value.rotation = _rotation;
                value.localRotation = _localRotation;
                value.localScale = _localScale;
            }
        }

        private struct RectTransformState : IComponentState
        {
            private readonly Vector2 _anchoredPosition;
            private readonly Vector3 _anchoredPosition3D;
            private readonly Vector2 _anchorMin;
            private readonly Vector2 _anchorMax;
            private readonly Vector2 _pivot;
            private readonly Vector2 _sizeDelta;
            private readonly Vector2 _offsetMin;
            private readonly Vector2 _offsetMax;
            private readonly Vector3 _position;
            private readonly Quaternion _rotation;
            private readonly Vector3 _localScale;

            public RectTransformState(RectTransform value)
            {
                _anchoredPosition = value.anchoredPosition;
                _anchoredPosition3D = value.anchoredPosition3D;
                _anchorMin = value.anchorMin;
                _anchorMax = value.anchorMax;
                _pivot = value.pivot;
                _sizeDelta = value.sizeDelta;
                _offsetMin = value.offsetMin;
                _offsetMax = value.offsetMax;
                _position = value.position;
                _rotation = value.rotation;
                _localScale = value.localScale;
            }

            public void Restore(Component component)
            {
                var value = component as RectTransform;
                if (value == null) return;
                value.anchorMin = _anchorMin;
                value.anchorMax = _anchorMax;
                value.pivot = _pivot;
                value.sizeDelta = _sizeDelta;
                value.offsetMin = _offsetMin;
                value.offsetMax = _offsetMax;
                value.anchoredPosition = _anchoredPosition;
                value.anchoredPosition3D = _anchoredPosition3D;
                value.position = _position;
                value.rotation = _rotation;
                value.localScale = _localScale;
            }
        }

        private struct ImageState : IComponentState
        {
            private readonly Color _color;
            private readonly float _fillAmount;

            public ImageState(Image value)
            {
                _color = value.color;
                _fillAmount = value.fillAmount;
            }

            public void Restore(Component component)
            {
                var value = component as Image;
                if (value == null) return;
                value.color = _color;
                value.fillAmount = _fillAmount;
            }
        }

        private struct CanvasGroupState : IComponentState
        {
            private readonly float _alpha;
            private readonly bool _interactable;
            private readonly bool _blocksRaycasts;
            private readonly bool _ignoreParentGroups;

            public CanvasGroupState(CanvasGroup value)
            {
                _alpha = value.alpha;
                _interactable = value.interactable;
                _blocksRaycasts = value.blocksRaycasts;
                _ignoreParentGroups = value.ignoreParentGroups;
            }

            public void Restore(Component component)
            {
                var value = component as CanvasGroup;
                if (value == null) return;
                value.alpha = _alpha;
                value.interactable = _interactable;
                value.blocksRaycasts = _blocksRaycasts;
                value.ignoreParentGroups = _ignoreParentGroups;
            }
        }

        private struct TextMeshProUGUIState : IComponentState
        {
            private readonly Color _color;
            private readonly Color32 _faceColor;
            private readonly Color32 _outlineColor;
            private readonly float _fontSize;
            private readonly int _maxVisibleCharacters;
            private readonly int _maxVisibleWords;
            private readonly int _maxVisibleLines;
            private readonly float _characterSpacing;
            private readonly float _wordSpacing;
            private readonly float _lineSpacing;
            private readonly float _paragraphSpacing;

            public TextMeshProUGUIState(TextMeshProUGUI value)
            {
                _color = value.color;
                _faceColor = value.faceColor;
                _outlineColor = value.outlineColor;
                _fontSize = value.fontSize;
                _maxVisibleCharacters = value.maxVisibleCharacters;
                _maxVisibleWords = value.maxVisibleWords;
                _maxVisibleLines = value.maxVisibleLines;
                _characterSpacing = value.characterSpacing;
                _wordSpacing = value.wordSpacing;
                _lineSpacing = value.lineSpacing;
                _paragraphSpacing = value.paragraphSpacing;
            }

            public void Restore(Component component)
            {
                var value = component as TextMeshProUGUI;
                if (value == null) return;
                value.color = _color;
                value.faceColor = _faceColor;
                value.outlineColor = _outlineColor;
                value.fontSize = _fontSize;
                value.maxVisibleCharacters = _maxVisibleCharacters;
                value.maxVisibleWords = _maxVisibleWords;
                value.maxVisibleLines = _maxVisibleLines;
                value.characterSpacing = _characterSpacing;
                value.wordSpacing = _wordSpacing;
                value.lineSpacing = _lineSpacing;
                value.paragraphSpacing = _paragraphSpacing;
            }
        }
    }
}