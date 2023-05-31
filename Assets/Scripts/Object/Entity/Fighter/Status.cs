using System;
using Object.Element;
using Object.Sword;

namespace Object.Entity.Fighter
{
  [Serializable]
  public struct Status
  {
    public float hp;

    public float maxHp;

    public float damage;

    public float moveSpeed;

    public float jumpPower;

    public ElementSet[] elementSetting;
  }
}