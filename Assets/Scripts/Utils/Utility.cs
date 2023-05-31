using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
  public static class Utility
  {
    public static T GetTypeProperty<T>(this Type obj, string propertyName = "Type") where T : Enum
    {
      var type = obj.GetProperty(propertyName);
      if (type is null)
        throw new Exception("Object type is null.");
      return (T)type.GetValue(null);
    }

    public static void StopNStartCoroutine(this MonoBehaviour sender, ref Coroutine valCoroutine, IEnumerator coroutine)
    {
      if (valCoroutine is not null) sender.StopCoroutine(valCoroutine);
      valCoroutine = sender.StartCoroutine(coroutine);
    }
  }
}