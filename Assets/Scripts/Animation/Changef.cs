﻿using System;
using System.Collections;
using UnityEngine;

namespace Animation
{
  public class Changef : BaseAnimation<float>
  {
    private float start;
    private float end;

    protected override IEnumerator AnimationRoutine()
    {
      var timer = 0f;
      SetValue(start);
      
      while (!Mathf.Approximately(Value, end))
      {
        yield return new WaitForEndOfFrame();
        SetValue(Mathf.Lerp(start, end, timer));
        timer += Time.unscaledDeltaTime * Speed;
      }
    }

    public void Start(float startValue, float endValue, float? speed = null)
    {
      start = startValue;
      end = endValue;

      base.Start(speed);
    }
    
    public Changef(MonoBehaviour sender, Action<float> onValueChange, float defaultValue)
      : base(sender, onValueChange, defaultValue)
    {
    }
  }
}