﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Utils
{
  public static class Utility
  {
    public static void SetCursorPosToObject(Transform transform)
      => SetCursorPos(transform.position.WorldToScreenPoint());

    public static void SetCursorPos(Vector2 position)
      => Mouse.current.WarpCursorPosition(position);

    public static Vector2 WorldToScreenPoint(this Vector2 world) => MainCam.WorldToScreenPoint(world);

    public static Vector2 ScreenToWorldPoint(this Vector2 world) => MainCam.ScreenToWorldPoint(world);

    public static Vector3 WorldToScreenPoint(this Vector3 world) => MainCam.WorldToScreenPoint(world);

    public static Vector3 ScreenToWorldPoint(this Vector3 world) => MainCam.ScreenToWorldPoint(world);

    public static UnityEngine.Camera MainCam => UnityEngine.Camera.main;

    public static T GetTypeProperty<T>(this Type obj, string propertyName = "Type") where T : Enum
    {
      var type = obj.GetProperty(propertyName);
      if (type is null)
        throw new Exception("Object type is null.");
      return (T) type.GetValue(null);
    }

    public static void StopNStartCoroutine(this MonoBehaviour sender, ref Coroutine valCoroutine, IEnumerator coroutine)
    {
      if (valCoroutine is not null) sender.StopCoroutine(valCoroutine);
      valCoroutine = sender.StartCoroutine(coroutine);
    }

    public static T Random<T>(this IEnumerable<T> enumerable)
    {
      var enumerable1 = enumerable as T[] ?? enumerable.ToArray();
      return enumerable1[UnityEngine.Random.Range(0, enumerable1.Count())];
    }

    public static bool IsEqual(this Color a, Color b) =>
      Mathf.Approximately(a.a, b.a) && Mathf.Approximately(a.r, b.r) &&
      Mathf.Approximately(a.g, b.g) && Mathf.Approximately(a.b, b.b);

    public static bool IsEqual(this Vector2 a, Vector2 b) =>
      Mathf.Approximately(a.x, b.x) && Mathf.Approximately(a.y, b.y);

    public static bool IsEqual(this Vector3 a, Vector3 b) =>
      Mathf.Approximately(a.x, b.x) && Mathf.Approximately(a.y, b.y) && Mathf.Approximately(a.z, b.z);

    public static Color Setter(this Color color, float? r = null, float? g = null, float? b = null, float? a = null)
    {
      Color result = color;

      if (r is not null) result.r = r.Value;
      if (g is not null) result.g = g.Value;
      if (b is not null) result.b = b.Value;
      if (a is not null) result.a = a.Value;

      return result;
    }

    public static Vector3 Setter(this Vector3 vector3, float? x = null, float? y = null, float? z = null)
    {
      var result = vector3;

      if (x is not null) result.x = x.Value;
      if (y is not null) result.y = y.Value;
      if (z is not null) result.z = z.Value;

      return result;
    }

    public static Vector2 Setter(this Vector2 vector2, float? x = null, float? y = null)
    {
      var result = vector2;

      if (x is not null) result.x = x.Value;
      if (y is not null) result.y = y.Value;

      return result;
    }

    public static Vector3 WorldToScreenSpace(this RectTransform canvas, Vector3 worldPos)
    {
      Vector3 screenPoint = MainCam.WorldToScreenPoint(worldPos);
      screenPoint.z = 0;

      if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, screenPoint, MainCam, out var screenPos))
        return screenPos;

      return screenPoint;
    }
  }
}
