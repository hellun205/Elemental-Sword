using System;
using System.Collections;
using UnityEngine;

namespace Animation
{
  public class ChangeColorSmooth : BaseAnimation<ChangeColorSmooth, Color>
  {
    private Color start;
    private Color end;

    protected override IEnumerator AnimationRoutine()
    {
      SetValue(start);

      while (!Value.Equals(end))
      {
        yield return new WaitForEndOfFrame();
        SetValue(Color.Lerp(Value, end, DeltaTime * Speed));
      }

      CallEndEvent();
    }

    public void Start(Color startColor, Color endColor, float? speed = null)
    {
      start = startColor;
      end = endColor;

      base.Start(speed);
    }

    public ChangeColorSmooth(MonoBehaviour sender, Action<Color> onValueChange, Color defaultValue,
      bool isUnscaled = false) : base(sender, onValueChange, defaultValue, isUnscaled)
    {
    }
  }
}