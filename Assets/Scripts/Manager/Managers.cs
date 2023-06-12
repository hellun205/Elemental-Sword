﻿using Audio;
using Element;
using Object;
using Object.Entity;
using Object.Entity.Fighter.Player;
using Player;
using UnityEngine;

namespace Manager
{
  public class Managers : GameObjectSingleTon<Managers>
  {
    public static KeyManager Key { get; private set; }
    public static ElementManager Element { get; private set; }
    public static PrefabManager Prefab { get; private set; }
    public static PlayerController Player { get; private set; }
    public static Player.Player PlayerM { get; private set; }
    public static AudioManager Audio { get; private set; }
    public static EntityManager Entity { get; private set; }
    
    protected override void Awake()
    {
      base.Awake();
      Key = FindObjectOfType<KeyManager>();
      Element = ElementManager.Instance;
      Prefab = FindObjectOfType<PrefabManager>();
      Player = FindObjectOfType<PlayerController>();
      PlayerM = FindObjectOfType<Player.Player>();
      Audio = FindObjectOfType<AudioManager>();
      Entity = FindObjectOfType<EntityManager>();
    }
  }
}