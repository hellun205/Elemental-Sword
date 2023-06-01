using System;
using System.Linq;
using Animation;
using Camera;
using Manager;
using Object.Element;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
  public class PlayerController : SingleTon<PlayerController>
  {
    private const float CloseSize = 0.1f;
    private const float OpenSize = 1f;
    private const float CloseSpeed = 9f;
    private const float OpenSpeed = 9f;

    public ElementType currentElement;

    private CanvasGroup selectorCanvas;

    private Image elementContainer;

    // Animations
    private Changef alphaAnim;

    private ChangefSmooth sizeAnim;

    private Object.Entity.Fighter.Player.Player player;
    private UnityEngine.Camera mainCamera;

    private bool isActive;

    protected override void Awake()
    {
      base.Awake();
      player = FindObjectOfType<Object.Entity.Fighter.Player.Player>();
      mainCamera = UnityEngine.Camera.main;
      selectorCanvas = GameObject.Find("@Elemental Swiper").GetComponent<CanvasGroup>();
      elementContainer = GameObject.Find("@Elements").GetComponent<Image>();
      selectorCanvas.alpha = 0f;

      alphaAnim = new Changef(this, value => { selectorCanvas.alpha = value; }, 0f);

      sizeAnim = new ChangefSmooth(this,
        value => { elementContainer.transform.localScale = new Vector3(value, value); }, CloseSize);
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
        alphaAnim.Start(0f, 1f, OpenSpeed);
        sizeAnim.Start(CloseSize, OpenSize, OpenSpeed);
      }
    }

    private void CloseSelector()
    {
      if (isActive)
      {
        isActive = false;
        currentElement = ElementSelector.SelectedElement;
        alphaAnim.Start(1f, 0f, CloseSpeed);
        sizeAnim.Start(OpenSize, CloseSize, CloseSpeed);
        SetElement(currentElement);
      }
    }

    private void SetElement(ElementType element)
    {
      ScreenFireEffect.instance.SetVisibility(element != ElementType.None);
      if (element == ElementType.None) return;
      
      var color = ElementMgr.instance.elements.Single(x => x.type == element).color;

      ScreenFireEffect.instance.ChangeColor(color);
    }
  }
}
