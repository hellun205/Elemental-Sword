using System;
using UnityEngine;

namespace Object.Entity.Fighter
{
  [Serializable]
  public struct Status
  {
    public float hp;

    public float maxHp;

    public float damage;

    [SerializeField]
    private float _moveSpeed;

    public float moveSpeed
    {
      get => Mathf.Max(_moveSpeed, 0);
      set => _moveSpeed = value;
    }

    [SerializeField]
    private float _jumpPower;

    public float jumpPower
    {
      get => Mathf.Max(_jumpPower, 0);
      set => _jumpPower = value;
    }

    public const float SlowPercent = 0.3f;

    [HideInInspector]
    public bool isSlowed;

    [HideInInspector]
    public float SlowedValue;
  }
}
