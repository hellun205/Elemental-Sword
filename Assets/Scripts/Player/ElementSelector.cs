using Animation;
using Object.Element;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Player
{
  [RequireComponent(typeof(Image))]
  public class ElementSelector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
  {
    public static ElementType SelectedElement { get; private set; }
    
    private const float EnterSize = 1.4f;

    private const float NormalSize = 1f;

    private const float EnterAlpha = 0.8f;

    private const float NormalAlpha = 0.4f;

    private const float EnterAnimSpeed = 6f;

    private const float ExitAnimSpeed = 9f;

    [SerializeField]
    private Image img;

    [SerializeField]
    private ElementType element;

    // Animations
    private ChangefSmooth sizeAnim;
    private ChangefSmooth alphaAnim;

    private void Reset()
    {
      img = GetComponent<Image>();
      var clr = img.color;
      clr.a = NormalAlpha;
      img.color = clr;
      transform.localScale = new Vector3(NormalSize, NormalSize);
    }

    private void Awake()
    {
      img.alphaHitTestMinimumThreshold = 1f;
      sizeAnim = new ChangefSmooth(this, value => transform.localScale = new Vector3(value, value), NormalSize);
      alphaAnim = new ChangefSmooth(this, value =>
      {
        var color = img.color;
        color.a = value;
        img.color = color;
      }, NormalAlpha);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
      sizeAnim.Start(transform.localScale.x, EnterSize, EnterAnimSpeed);
      alphaAnim.Start(img.color.a, EnterAlpha, EnterAnimSpeed);
      SelectedElement = element;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
      sizeAnim.Start(transform.localScale.x, NormalSize, ExitAnimSpeed);
      alphaAnim.Start(img.color.a, NormalAlpha, ExitAnimSpeed);
      SelectedElement = ElementType.None;
    }
  }
}