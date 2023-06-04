using System;
using Utils;

namespace Object.Entity.Fighter
{
  public class StateCoroutiner
  {
    public Coroutiner coroutine;

    public bool isActive = false;

    public float duration;

    public StateCoroutiner(Coroutiner coroutine, float duration)
    {
      this.coroutine = coroutine;
      this.duration = duration;
    }
  }
}
