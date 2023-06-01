using System;
using System.Collections;
using UnityEngine;
using Utils;

namespace Animation
{
  public abstract class BaseAnimation<T, TValue> where T : BaseAnimation<T, TValue>
  {
    public delegate void AnimationEventListener(T sender);

    public event AnimationEventListener OnStartedAnimation;
    public event AnimationEventListener OnEndAnimation;

    protected MonoBehaviour sender { get; private set; }

    public BaseCoroutine AnimCoroutine { get; private set; }

    public TValue Value { get; private set; }

    public float Speed { get; set; } = 1f;

    protected abstract IEnumerator AnimationRoutine();

    private Action<TValue> onValueChange;

    protected BaseAnimation(MonoBehaviour sender, Action<TValue> onValueChange, TValue defaultValue)
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
      CallStartedEvent();
    }

    public void Stop() => AnimCoroutine.Stop();

    public void SetValue(TValue value)
    {
      Value = value;
      onValueChange.Invoke(Value);
    }
    
    protected void CallStartedEvent() => OnStartedAnimation?.Invoke((T)this);
    protected void CallEndEvent() => OnEndAnimation?.Invoke((T)this);
  }
}