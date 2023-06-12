using Object.Pool;
using UnityEngine;
using Utils;

namespace Object.Entity
{
  public abstract class UIEntity : Entity
  {
    protected RectTransform rectTransform { get; private set; }

    private Vector3 _position;
    
    public new static RectTransform container;

    public override Vector2 position
    {
      get => _position;
      set
      {
        _position = value;
        rectTransform.anchoredPosition = container.WorldToScreenSpace(_position);
      }
    }

    protected virtual void Awake()
    {
      rectTransform = GetComponent<RectTransform>();
      container ??= GameObject.Find("UIEntityContainer").GetComponent<RectTransform>();
    }
  }
}
