using System;
using Manager;
using Object.Entity.Fighter.Player;
using UnityEngine;

namespace Camera
{
  public sealed class MainCamera : SingleTon<MainCamera>
  {
    private Transform target;
    
    protected override void Awake()
    {
      base.Awake();
      target = FindObjectOfType<PlayerController>().transform;
    }
    
    
  }
}