using UnityEngine;

namespace Manager
{
  public class KeyManager : SingleTon<KeyManager>
  {
    public KeyCode changeElement = KeyCode.E;

    public KeyCode jump = KeyCode.Space;
  }
}