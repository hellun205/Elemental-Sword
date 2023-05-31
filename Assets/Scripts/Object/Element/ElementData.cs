using System;
using UnityEngine;

namespace Object.Element
{
  [Serializable]
  public struct ElementData
  {
    [Header("Fire")]
    public byte curDotCount;

    public byte maxDotCount;

    public float dotDmg;

    public float dotSpeed;

    public Coroutine dotCoroutine;
    
  }
}