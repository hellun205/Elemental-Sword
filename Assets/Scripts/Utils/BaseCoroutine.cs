using System;
using System.Collections;
using UnityEngine;

namespace Utils
{
  public class BaseCoroutine
  {
    public MonoBehaviour Sender { get; }

    public Coroutine Instance;

    public Func<IEnumerator> Routine;
    
    public BaseCoroutine(MonoBehaviour sender, Func<IEnumerator> routine)
    {
      Sender = sender;
      this.Routine = routine;
    }

    public void Start()
    {
      Stop();
      Instance = Sender.StartCoroutine(Routine.Invoke());
    }

    public void Stop()
    {
      if (Instance is not null)
        Sender.StopCoroutine(Instance);
    }
  }
}