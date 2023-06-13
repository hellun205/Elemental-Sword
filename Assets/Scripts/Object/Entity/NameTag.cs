using System;
using System.Collections;
using Manager;
using Object.Entity;
using UnityEngine;

namespace Entity
{
  /// <summary>
  /// 엔티티의 이름을 표시 하는 컴포넌트
  /// </summary>
  public class NameTag : MonoBehaviour
  {
    public string display;
    
    /// <summary>
    /// 이름표 엔티티
    /// </summary>
    private UEText displayText;

    /// <summary>
    /// 이름표를 표시 할 엔티티
    /// </summary>
    private Object.Entity.Entity entity;

    [SerializeField]
    private Collider2D col;

    [SerializeField]
    private float distance = 0.1f;

    private float colDistance;

    private Vector2 GetPos() => new Vector2(entity.position.x, entity.position.y + colDistance + distance);

    private void Awake()
    {
      entity = GetComponent<Object.Entity.Entity>();
      colDistance = col.bounds.extents.y;
      EntityManager.Instance.onGetAfter += OnGetEntityEntity;
      EntityManager.Instance.onReleaseBefore += OnReleasedEntity;
    }

    private void Update()
    {
      displayText.text = display;
      displayText.position = GetPos();
    }

    public void OnGetEntityEntity(Object.Entity.Entity entity)
    {
      if (entity == this.entity)
        displayText = Managers.Entity.Get<UEText>(GetPos(), x => x.text = display);
    }

    public void OnReleasedEntity(Object.Entity.Entity entity)
    {
      if (entity == this.entity)
        displayText.Release();
    }

    private void OnDestroy()
    {
      EntityManager.Instance.onGetAfter -= OnGetEntityEntity;
      EntityManager.Instance.onReleaseBefore -= OnReleasedEntity;
    }
  }
}
