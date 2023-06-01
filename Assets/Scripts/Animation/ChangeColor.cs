using System;
using System.Collections;
using UnityEngine;

namespace Animation
{
  public class ChangeColor : BaseAnimation<Color>
  {
    private Color start;
    private Color end;
    
    protected override IEnumerator AnimationRoutine()
    {
      var timer = 0f;
      SetValue(start);
      
      while (!Value.Equals(end))
      {
        yield return new WaitForEndOfFrame();
        SetValue(Color.Lerp(start, end, timer));
        timer += Time.unscaledDeltaTime * Speed;
      }
    }

    public void Start(Color startColor, Color endColor, float? speed = null)
    {
      start = startColor;
      end = endColor;

      base.Start(speed);
    }
    
    public ChangeColor(MonoBehaviour sender, Action<Color> onValueChange, Color defaultValue)
      : base(sender, onValueChange, defaultValue)
    {
    }
  }
}