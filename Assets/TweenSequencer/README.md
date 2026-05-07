# TweenSequencer

Node-based сценарии для DOTween на базе xNode.

## Возможности

- Передача именованных параметров (ключ -> значение разных типов)
- Передача InitialParameters при старте последовательности (с override поверх runner initialParameters)
- Вызов методов компонентов по ключу (sync/coroutine, с опциональным ожиданием)
- Параллельное выполнение веток через `Parallel` node
- Работа с UGUI (`CanvasGroup`, `RectTransform`, `Graphic`)

## Зависимости

- DOTween
- xNode

## Структура

- `Assets/TweenSequencer/Runtime` - рантайм
- `Assets/TweenSequencer/Editor` - редакторные меню
- `Assets/TweenSequencer/Runtime/TransformNodes.cs` - ноды для `Transform`
- `Assets/TweenSequencer/Runtime/RectTransformNodes.cs` - ноды для `RectTransform`
- `Assets/TweenSequencer/Runtime/ImageNodes.cs` - ноды для `Image`
- `Assets/TweenSequencer/Runtime/CanvasGroupNodes.cs` - ноды для `CanvasGroup`
- `Assets/TweenSequencer/Runtime/TextMeshProUGUINodes.cs` - ноды для `TextMeshProUGUI`

## Быстрый старт

1. Создайте graph: `Assets/Create/TweenSequencer/Scenario Graph`.
2. Откройте graph в xNode editor.
3. Добавьте `Entry` node и соедините с нужными нодами.
4. На сцене создайте объект с `TweenScenarioRunner`.
5. Назначьте созданный graph в поле `Scenario`.
6. Добавьте в `initialParameters` ссылки на нужные компоненты/значения по ключам.
7. Запускайте через `runner.PlayCoroutine()` или `runner.Play()`.

## Именованные параметры

Параметры задаются через `NamedParameter`:

- `key` - имя параметра
- `type` - тип значения
- соответствующее поле значения (`floatValue`, `stringValue` и т.д.)

В `InvokeMethodNode` список `parameterKeys` определяет порядок передачи аргументов в метод.

Для tween-нод добавлена поддержка целевых значений из параметров:

- поля вида `toParameterKey`, `endValueParameterKey`, `jumpPowerParameterKey`
- если ключ заполнен и параметр найден, нода использует значение из контекста
- если ключ пустой или не найден, используется локальное поле ноды (`to`, `endValue`, и т.д.)

Примеры:

- `ImageDOFillAmountNode`: `toParameterKey` -> `float`
- `TransformDOMoveNode`: `toParameterKey` -> `Vector3` или `Transform` (берется `position`)
- `TransformDORotateNode`: `toParameterKey` -> `Vector3` или `Transform` (берется `rotation.eulerAngles`)

Дополнительно поддерживаются runtime-only типы параметров:

- `Action` (синхронный вызов)
- `Func<IEnumerator>` (асинхронная корутина)

Для них используйте `NamedParameter.FromAction(...)` и `NamedParameter.FromCoroutineFactory(...)` при запуске из кода.

## Вызов sync/coroutine методов

`InvokeMethodNode`:

- `targetParameterKey` - ключ параметра, в котором лежит компонент-цель
- `methodName` - имя метода
- `parameterKeys` - аргументы из контекста
- `waitForCompletion` - ждать завершения, если метод вернул `IEnumerator`/yield instruction

`InvokeParameterNode`:

- `parameterKey` - ключ параметра типа `Action` или `Func<IEnumerator>`
- `waitForCompletion` - ждать завершения для `Func<IEnumerator>`

Пример компонента:

```csharp
using System.Collections;
using UnityEngine;

public class UiActions : MonoBehaviour {
    public void ShowInstant(string message) {
        Debug.Log(message);
    }

    public IEnumerator ShowWithDelay(float seconds) {
        yield return new WaitForSeconds(seconds);
    }
}
```

## Параллельное выполнение

`ParallelNode` запускает `branchA`, `branchB`, `branchC` одновременно и ждет завершения всех веток.
После этого выполняются ноды из порта `next`.

## UGUI ноды

- Для каждой операции создан отдельный node class и отдельный пункт в CreateNodeMenu.
- Ноды разделены по группам таргетов в отдельных файлах (`Transform`, `RectTransform`, `Image`, `CanvasGroup`, `TextMeshProUGUI`).

Все ноды используют `targetParameterKey` и берут компонент-цель из `NamedParameter` типа `Object`.

## Расширение пакета

1. Создайте новую ноду, унаследовавшись от `TweenScenarioNode`.
2. Добавьте порты:
   - `[Input(backingValue = ShowBackingValue.Never)] public bool input;`
   - `[Output(backingValue = ShowBackingValue.Never)] public bool next;`
3. Реализуйте `Execute(TweenScenarioContext context)`.
4. Добавьте атрибут `[CreateNodeMenu("TweenSequencer/.../My Node")]`.

Пример:

```csharp
using System.Collections;
using UnityEngine;
using XNode;
using TweenSequencer.Runtime;

[CreateNodeMenu("TweenSequencer/Custom/Log")]
public class LogNode : TweenScenarioNode {
    [Input(backingValue = ShowBackingValue.Never)] public bool input;
    [Output(backingValue = ShowBackingValue.Never)] public bool next;
    public string text;

    public override IEnumerator Execute(TweenScenarioContext context) {
        Debug.Log(text);
        yield break;
    }
}
```

## Ограничения текущей реализации

- Для предотвращения циклических зависаний избегайте циклов в graph.
- `InvokeMethodNode` выбирает метод по имени и количеству аргументов.
- Поддержка асинхронности основана на Unity Coroutine (`IEnumerator`).
