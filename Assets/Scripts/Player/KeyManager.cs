using Manager;
using UnityEngine;

namespace Player
{
  public class KeyManager : GameObjectSingleTon<KeyManager>
  {
    public KeyCode changeElement = KeyCode.E;

    public KeyCode jump = KeyCode.Space;
  }
}