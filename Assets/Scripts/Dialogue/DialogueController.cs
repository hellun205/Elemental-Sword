using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogue
{
  public class DialogueController : MonoBehaviour
  {
    public TextMeshProUGUI tmp;
    public Image img;

    public void Open()
    {
      gameObject.SetActive(true);
    }

    public void Close()
    {
      gameObject.SetActive(false);
    }
  }
}