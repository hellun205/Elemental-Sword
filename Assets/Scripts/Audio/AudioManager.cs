using System.Collections.Generic;
using System.Linq;
using Manager;
using UnityEngine;

namespace Audio
{
  public class AudioManager : GameObjectSingleTon<AudioManager>
  {
    private GameObject sfxGO;

    private GameObject bgmGO;
    
    private List<AudioSource> sfxPlayer = new List<AudioSource>();

    private AudioSource bgmPlayer;
    
    private float bgmVol = 1f;
    private float sfxVol = 1f;
    public float BGMVolume
    {
      get => bgmVol;
      set
      {
        bgmVol = value;
        bgmPlayer.volume = value;
      }
    }

    public float SFXVolume
    {
      get => sfxVol;
      set
      {
        sfxVol = value;
        sfxPlayer.ForEach(src => src.volume = value);
      }
    }

    public AudioData[] bgmDatas;

    public AudioData[] sfxDatas;

    protected override void Awake()
    {
      base.Awake();
      if (bgmGO is null)
      {
        bgmGO = GameObject.Find("@BGM");
        if (bgmGO is null)
        {
          bgmGO = new GameObject();
          bgmGO.transform.SetParent(gameObject.transform);
        }
      }

      if (sfxGO is null)
      {
        sfxGO = GameObject.Find("@SFX");
        if (sfxGO is null)
        {
          sfxGO = new GameObject();
          sfxGO.transform.SetParent(gameObject.transform);
        }
      }

      var _component = bgmGO.GetComponent<AudioSource>();
      if (_component == null)
        bgmPlayer = bgmGO.AddComponent<AudioSource>();
      else
        bgmPlayer = _component;
    }

    public void PlayBGM(string bgmName)
    {
      var sound = bgmDatas.SingleOrDefault(data => data.name == bgmName);
      if (sound is not null)
        PlaySound(bgmPlayer, sound);
      else
        Debug.LogError($"Can't find BGM audio data: {bgmName}.");
    }
    
    public void PlaySFX(string sfxName)
    {
      var sound = sfxDatas.SingleOrDefault(data => data.name == sfxName);
      if (sound is not null)
      {
        if (sfxPlayer.Count == 0 || sfxPlayer.Count(source => !source.isPlaying) == 0)
          sfxPlayer.Add(sfxGO.AddComponent<AudioSource>());

        var player = sfxPlayer.First(source => !source.isPlaying);
        player.volume = sfxVol;
        PlaySound(player, sound);
      }
      else
        Debug.LogError($"Can't find SFX audio data: {sfxName}.");
    }

    private static void PlaySound(AudioSource source, AudioData audioData, float delay = 0f)
    {
      source.clip = audioData.audioClip;
      source.pitch = audioData.pitch;
      source.loop = audioData.loop;
      source.PlayDelayed(delay + audioData.delay);
    }
  }
}