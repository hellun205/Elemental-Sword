using System;
using UnityEngine;

namespace Object.Pool {
  public abstract class PoolManagement: MonoBehaviour
  {
    public abstract PoolType type { get; }

    public virtual Vector2 position {
      get => transform.position;
      set => transform.position = value;
    }
  }
}