using Animation;
using Animation.Combined;
using Object.Element;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utils;

namespace Player
{
  [RequireComponent(typeof(Image))]
  public class ElementSelector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
  {
    public static ElementType SelectedElement { get; private set; }

    private readonly Vector2 EnterSize = new(1.4f, 1.4f);

    private readonly Vector2 NormalSize = new(1f, 1f);

    private const float EnterAlpha = 0.8f;

    private const float NormalAlpha = 0.4f;

    private const float EnterAnimSpeed = 6f;

    private const float ExitAnimSpeed = 9f;

    [SerializeField]
    private Image img;

    [SerializeField]
    private ElementType element;

    // Animations
    private SmoothSizeAndFade anim;

    private void Reset()
    {
      img = GetComponent<Image>();
      img.color = img.color.Setter(a: NormalAlpha);
      transform.localScale = ((Vector3) NormalSize).Setter(z: 1);
    }

    private void Awake()
    {
      img.alphaHitTestMinimumThreshold = 1f;
      anim = new(this,
        new StructPointer<Vector3>(() => transform.localScale, value => transform.localScale = value),
        new StructPointer<float>(() => img.color.a, value => img.color = img.color.Setter(a: value)))
      {
        maxSize = EnterSize,
        minSize = NormalSize,
        maxAlpha = EnterAlpha,
        minAlpha = NormalAlpha,
        fadeShowAnimSpeed = EnterAnimSpeed,
        sizeShowAnimSpeed = EnterAnimSpeed,
        fadeHideAnimSpeed = ExitAnimSpeed,
        sizeHideAnimSpeed = ExitAnimSpeed,
      };
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
      anim.Show();
      SelectedElement = element;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
      anim.Hide();
      SelectedElement = ElementType.None;
    }
  }
}
