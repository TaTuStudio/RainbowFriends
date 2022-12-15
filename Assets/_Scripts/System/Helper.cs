#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using Object = UnityEngine.Object;

// using UnityEngine;
// using UnityEngine.EventSystems;

/// <summary>
/// A static class for general helpful methods
/// </summary>
public static class Helpers
{
    
    #region Unity
    /// <summary>
    /// Destroy all child objects of this transform (Unintentionally evil sounding).
    /// Use it like so:
    /// <code>
    /// transform.DestroyChildren();
    /// </code>
    /// </summary>
    public static void DestroyChildren(this Transform t)
    {
        foreach (Transform child in t)
            Object.Destroy(child.gameObject);
    }

    /// <summary>
    /// Convert Vector 3 to Vector 2
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static Vector2 ToV2(this Vector3 input) => new Vector2(input.x, input.y);
    
    /// <summary>
    /// None-allocating WaitForSeconds
    /// </summary>
    /// <param name="time in seconds"></param>
    /// <returns></returns>
    private static readonly Dictionary<float, WaitForSeconds> WaitDictionary =
        new Dictionary<float, WaitForSeconds>();

    public static WaitForSeconds NewWaitForSeconds(this float time)
    {
        if (WaitDictionary.TryGetValue(time, out var wait))
            return wait;
        WaitDictionary[time] = new WaitForSeconds(time);
        return WaitDictionary[time];
    }

    /// <summary>
    /// Is pointer over UI?
    /// </summary>
    /// <returns></returns>
    private static PointerEventData? pointerEventData;
    private static List<RaycastResult>? raycastResults;

    public static bool IsPointerOverUI()
    {
        pointerEventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };
        raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);
        return raycastResults.Count > 0;
    }

    /// <summary>
    /// Place an UI element to a world position
    /// _ui_canvas being the Canvas, _world_point being a point in the world
    /// var rect_transform = _ui_element.GetComponent<RectTransform>();
    /// rect_transform.anchoredPosition = _ui_canvas.WorldToCanvas(_world_point);
    /// </summary>
    public static Vector2 WorldToCanvas(this Canvas canvas, Vector3 worldPosition, Camera? camera = null)
    {
        if (camera == null)
        {
            camera = Camera.main;
        }

        var viewport_position = camera.WorldToViewportPoint(worldPosition);
        var canvas_rect = canvas.GetComponent<RectTransform>();

        var _sizeDelta = canvas_rect.sizeDelta;
        return new Vector2(
            (viewport_position.x * _sizeDelta.x) - (_sizeDelta.x * 0.5f),
            (viewport_position.y * _sizeDelta.y) - (_sizeDelta.y * 0.5f)
        );
    }
    #endregion


    #region CSharp
    /// <summary>
    /// Read the value in the line number, if null return empty.
    /// </summary>
    /// <param name="text"></param>
    /// <param name="lineNumber"></param>
    /// <returns></returns>
    public static string? ReadLine(this string text, int lineNumber)
    {
        var _reader = new StringReader(text);

        string? _line;
        var _currentLineNumber = 0;

        do
        {
            _currentLineNumber += 1;
            _line = _reader.ReadLine();
        } while (_line != null && _currentLineNumber < lineNumber);

        return (_currentLineNumber == lineNumber) ? _line : string.Empty;
    }
    
    /// <summary>
    /// Await an async void 
    /// </summary>
    public static async void Awaiter(this Task task, Action completeCallback, Action<Exception> errorCallback)
    {
        try
        {
            await task;
            completeCallback?.Invoke();
        }
        catch (Exception _exception)
        {
            errorCallback?.Invoke(_exception);
        }
    }
    #endregion
    
}