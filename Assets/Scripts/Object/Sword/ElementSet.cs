using System;
using UnityEngine;

namespace Object.Sword
{
  [Serializable]
  public struct ElementSet
  {
    public Element element;

    [Range(0f, 1f)]
    public float resistance;

    [Min(0f)]
    public float damage;
  }
}