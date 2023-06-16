using Manager;
using UnityEngine;

namespace Dialogue
{
  public class DialogueManager : GameObjectSingleTon<DialogueManager>
  {
    [SerializeField]
    private GameObject panel;

    public DialogueData a;
    // public void Open(AvartarDirection dir, Sprite avartar, )
  }
}