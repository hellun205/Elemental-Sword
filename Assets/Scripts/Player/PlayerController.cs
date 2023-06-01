using System;
using Animation;
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
        Open();
      else if (selectorCanvas.alpha >= 1f)
        Close();

      if (selectorCanvas.alpha == 0f) return;

      var pos = mainCamera.WorldToScreenPoint(player.transform.position);
      elementContainer.transform.position =
        Vector3.Lerp(elementContainer.transform.position, pos, Time.unscaledDeltaTime * 10f);
    }

    private void Open()
    {
      if (!isActive)
      {
        isActive = true;
        alphaAnim.Start(0f, 1f, OpenSpeed);
        sizeAnim.Start(CloseSize, OpenSize, OpenSpeed);
      }
    }

    private void Close()
    {
      if (isActive)
      {
        isActive = false;
        Debug.Log(ElementSelector.SelectedElement);
        currentElement = ElementSelector.SelectedElement;
        alphaAnim.Start(1f, 0f, CloseSpeed);
        sizeAnim.Start(OpenSize, CloseSize, CloseSize);
      }
    }
  }
}