using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Dialogue
{
  [Serializable]
  public struct DialogueData
  {
    public AvartarDirection direction;

    public Sprite avartar;

    public TimingText[] texts;

    public DialogueData(AvartarDirection direction, Sprite avartar) : this()
    {
      this.direction = direction;
      this.avartar = avartar;
    }

    public DialogueData(AvartarDirection direction, Sprite avartar, TimingText[] texts) : this(direction, avartar)
    {
      this.texts = texts;
    }

    public DialogueData(TimingText[] texts) : this()
    {
      this.texts = texts;
    }
  }
}