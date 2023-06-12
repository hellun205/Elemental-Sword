using System;
using UnityEngine;

namespace Audio
{
  [Serializable]
  public class AudioData
  {
    public string name;

    public AudioClip audioClip;

    [Range(0f, 3f)]
    public float pitch;

    [Range(0f, 3f)]
    public float delay;

    public bool loop;

    public AudioData(string name, AudioClip audioClip, float pitch = 1f, float delay = 0, bool loop = false)
    {
      this.name = name;
      this.audioClip = audioClip;
      this.pitch = pitch;
      this.delay = delay;
      this.loop = loop;
    }
  }
}
