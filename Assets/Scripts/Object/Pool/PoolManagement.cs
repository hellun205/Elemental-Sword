using System;
using UnityEngine;

namespace Object.Pool {
  public abstract class PoolManagement: MonoBehaviour
  {
    [NonSerialized]
    public string type;
    
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
  }
}