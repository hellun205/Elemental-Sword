using Player;
using UnityEngine;

namespace Manager
{
  public class Managers : GameObjectSingleTon<Managers>
  {
    public static KeyManager Key { get; private set; }
    
    protected override void Awake()
    {
      base.Awake();
      Key = FindObjectOfType<KeyManager>();
    }
  }
}