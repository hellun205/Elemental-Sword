using System;
using UnityEngine;

namespace Object.Pool {
  public class PoolManagement : MonoBehaviour {
    [HideInInspector]
    public PoolType type;

    public virtual Vector2 position {
      get => transform.position;
      set => transform.position = value;
    }
  }
}