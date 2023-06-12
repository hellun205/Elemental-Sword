using System;
using Manager;
using UnityEngine;
using UnityEngine.Pool;

namespace Object.Pool {
  public abstract class PoolManagement: MonoBehaviour
  {
    public string type { get; set; }
    
    public IObjectPool<PoolManagement> pool { get; set; }

    public virtual Vector2 position {
      get => transform.position;
      set => transform.position = value;
    }

    public virtual void OnReleased()
    {
      
    }
    
    public virtual void OnGet()
    {
      
    }

    public void Release() => pool.Release(this);
  }
}