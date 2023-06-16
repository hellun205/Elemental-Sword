using System;
using UnityEngine;

namespace Dialogue
{
  [Serializable]
  public struct DialogueData
  {
    public AvartarDirection direction;

    public Sprite avartar;

    public TimingText[] timingText;
  }
}