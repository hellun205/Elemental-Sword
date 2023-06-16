using System;

namespace Dialogue
{
  [Serializable]
  public struct TimingText
  {
    public float delay;
    public float speed;
    public string text;

    public TimingText(string text,float speed = 0.1f) : this()
    {
      delay = 0f;
      this.speed = speed;
      this.text = text;
    }

    public TimingText(float delay, string text, float speed = 0.1f) : this(text, speed)
    {
      this.delay = delay;
    }
  }
}