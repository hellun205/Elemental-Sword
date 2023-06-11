using System;
using Manager;
using Object.Entity.Fighter.Player;
using UnityEngine;
using Utils;

namespace Camera
{
  public sealed class MainCamera : GameObjectSingleTon<MainCamera>
  {
    private Transform target;

    protected override void Awake()
    {
      base.Awake();
      target = FindObjectOfType<PlayerController>().transform;
    }

    private void Update()
    {
      transform.position =
        Vector3.Lerp(transform.position, target.position.Setter(z: transform.position.z), Time.deltaTime * 3f);
    }
  }
}
