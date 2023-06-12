using System;
using Object.Pool;
using UnityEngine;

namespace Object.Entity
{
  public abstract class Entity : PoolManagement
  {
    public static Transform container;

    protected virtual void Awake()
    {
      container ??= GameObject.Find("EntityContainer").transform;
    }
  }
}
