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
      set
      {
        _moveSpeed = value;
        movement.moveSpeed = value;
      }
    }

    [SerializeField]
    private float _jumpPower;

    public float jumpPower
    {
      get => Mathf.Max(_jumpPower, 0);
      set
      {
        _jumpPower = value;
        movement.jumpPower = value;
      }
    }

    public Movement movement;

    public const float SlowPercent = 0.3f;

    [NonSerialized]
    public bool isSlowed;

    [NonSerialized]
    public float SlowedValue;

    public const float BurningSpeed = 0.6f;
    
    public const float FastBurningSpeed = 0.35f;

    [NonSerialized]
    public bool isBurning;
    
    [NonSerialized]
    public bool isFastBurning;

    public float burningSpeed => isFastBurning ? FastBurningSpeed : BurningSpeed;
    
    [NonSerialized]
    public bool isStrongBurn;

    public float burningDamage => maxHp * (isStrongBurn ? 0.09f : 0.05f);

    public void Heal(float amount) => hp = Mathf.Min(maxHp, hp + Mathf.Abs(amount));
    
    public void HealPercent(float percent) => hp = Mathf.Min(maxHp, hp + (maxHp * percent));
  }
}
