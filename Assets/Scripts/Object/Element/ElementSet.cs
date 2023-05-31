using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Object.Element
{
  [Serializable]
  public struct ElementSet
  {
    public ElementType elementType;

    [Range(0f, 1f)]
    public float resistance;

    [Range(0f, 1f)]
    public float armor;
  }
}