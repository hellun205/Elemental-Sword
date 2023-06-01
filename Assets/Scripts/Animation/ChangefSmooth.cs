using System;
using System.Collections;
using UnityEngine;

namespace Animation
{
  public class ChangefSmooth : BaseAnimation<ChangefSmooth,float>
  {
    private float start;
    private float end;

    protected override IEnumerator AnimationRoutine()
    {
      SetValue(start);
      
      while (!Mathf.Approximately(Value, end))
      {
        yield return new WaitForEndOfFrame();
        SetValue(Mathf.Lerp(Value, end, Time.unscaledDeltaTime * Speed));
      }
      CallEndEvent();
    }

    public void Start(float startValue, float endValue, float? speed = null)
    {
      start = startValue;
      end = endValue;

      base.Start(speed);
    }
    
    public ChangefSmooth(MonoBehaviour sender, Action<float> onValueChange, float defaultValue)
      : base(sender, onValueChange, defaultValue)
    {
    }
  }
}