using System;
using System.Collections;
using UnityEngine;
using Utils;

namespace Animation
{
  public abstract class BaseAnimation<T>
  {
    protected MonoBehaviour sender { get; private set; }

    public BaseCoroutine AnimCoroutine { get; private set; }

    public T Value { get; private set; }

    public float Speed { get; set; } = 1f;

    protected abstract IEnumerator AnimationRoutine();

    private Action<T> onValueChange;

    protected BaseAnimation(MonoBehaviour sender, Action<T> onValueChange, T defaultValue)
    {
      this.sender = sender;
      this.onValueChange = onValueChange;
      SetValue(defaultValue);
      AnimCoroutine = new BaseCoroutine(sender, AnimationRoutine);
    }

    public void Start(float? speed = null)
    {
      if (speed is not null)
        Speed = speed.Value;
      AnimCoroutine.Start();
    }

    public void Stop() => AnimCoroutine.Stop();

    public void SetValue(T value)
    {
      Value = value;
      onValueChange.Invoke(Value);
    }
  }
}