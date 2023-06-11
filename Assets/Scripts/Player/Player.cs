using Animation.Preset;
using Camera;
using Element;
using Manager;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Player
{
  public class Player : GameObjectSingleTon<Player>
  {
    private readonly Vector2 CloseSize = new(.1f, .1f);
    private readonly Vector2 OpenSize = new(1f, 1f);
    private const float CloseSpeed = 9f;
    private const float OpenSpeed = 9f;

    // ToDo : player interactable elements 
    public bool interactable => true;

    public SingleElement
      currentElement;

    private CanvasGroup selectorCanvas;

    private Image elementContainer;

    // Animations
    private PanelVisibler anim;

    private Object.Entity.Fighter.Player.PlayerController player;
    private UnityEngine.Camera mainCamera;

    private bool isActive;

    protected override void Awake()
    {
      base.Awake();
      player = FindObjectOfType<Object.Entity.Fighter.Player.PlayerController>();
      mainCamera = UnityEngine.Camera.main;
      selectorCanvas = GameObject.Find("@Elemental Swiper").GetComponent<CanvasGroup>();
      elementContainer = GameObject.Find("@Elements").GetComponent<Image>();
      selectorCanvas.alpha = 0f;

      anim = new(this, elementContainer.transform, selectorCanvas)
      {
        animation =
        {
          maxAlpha = 1f,
          minAlpha = 0f,
          fadeShowAnimSpeed = OpenSpeed,
          maxSize = OpenSize,
          minSize = CloseSize,
          sizeShowAnimSpeed = OpenSpeed,
        }
      };
    }

    private void Update()
    {
      if (Input.GetKey(Managers.Key.changeElement))
        OpenSelector();
      else if (selectorCanvas.alpha >= 1f)
        CloseSelector();

      if (selectorCanvas.alpha == 0f) return;

      var pos = mainCamera.WorldToScreenPoint(player.transform.position);
      elementContainer.transform.position =
        Vector3.Lerp(elementContainer.transform.position, pos, Time.unscaledDeltaTime * 10f);
    }

    private void OpenSelector()
    {
      if (!isActive)
      {
        isActive = true;
        elementContainer.transform.position = mainCamera.WorldToScreenPoint(player.transform.position);
        anim.Show();
        Utility.SetCursorPosToObject(player.transform);
      }
    }

    private void CloseSelector()
    {
      if (isActive)
      {
        isActive = false;
        currentElement = ElementSelector.SelectedElement;
        anim.Hide();
        SetElement(currentElement);
      }
    }

    private void SetElement(SingleElement element)
    {
      ScreenFireEffect.Instance.SetVisibility(element != SingleElement.None);
      if (element == SingleElement.None) return;

      var color = Managers.Element.elementSetting[element].color;

      ScreenFireEffect.Instance.ChangeColor(color);
    }
  }
}
