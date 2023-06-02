using System;
using System.Collections;
using UnityEngine;

namespace Animation
{
  public class FadeSmooth : BaseAnimation<FadeSmooth, Color>
  {
    private bool isFadeIn;

    protected override IEnumerator AnimationRoutine()
    {
      var to = isFadeIn ? 1f : 0f;
      while (!Mathf.Approximately(Value.a, to))
      {
        yield return new WaitForEndOfFrame();
        var color = Value;
        color.a = Mathf.Lerp(color.a, to, DeltaTime * Speed);
        SetValue(color);
      }

      CallEndEvent();
    }

    public void In(float? speed = null)
    {
      isFadeIn = true;
      Start(speed);
    }

    public void Out(float? speed = null)
    {
      isFadeIn = false;
      Start(speed);
    }


    public FadeSmooth(MonoBehaviour sender, Action<Color> onValueChange, Color defaultValue, bool isUnscaled = false) :
      base(sender, onValueChange, defaultValue, isUnscaled)
    {
    }
  }
}