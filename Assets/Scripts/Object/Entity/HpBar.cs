﻿using Entity.UI;
using Manager;
using Object.Pool;
using UnityEngine;

namespace Object.Entity
{
  public class HpBar : MonoBehaviour
  {
    private Entity entity;
    private UEHpBar hpBar;

    public float curHp;
    public float maxHp;

    [SerializeField]
    private Collider2D col;

    [SerializeField]
    private float distance = 0.1f;

    private float colDistance;

    private Vector2 GetPos() => new Vector2(
      entity.position.x,
      entity.position.y + colDistance + distance
    );

    private void Awake()
    {
      entity = GetComponent<Entity>();
      colDistance = col.bounds.extents.y;
      // EntityManager.Instance.onGetEntityAfter += OnGetEntityEntity;
      // EntityManager.Instance.onReleaseEntityBefore += OnReleasedEntity;
      EntityManager.Instance.onGetAfter += OnGetEntityEntity;
      EntityManager.Instance.onReleaseBefore += OnReleasedEntity;
    }

    private void Update()
    {
      hpBar.position = GetPos();
      hpBar.maxValue = maxHp;
      hpBar.value = curHp;
    }

    public void OnGetEntityEntity(Entity entity)
    {
      if (entity == this.entity)
        LoadHpBar();
    }

    public void OnReleasedEntity(Entity entity)
    {
      if (entity == this.entity)
        hpBar.Release();
    }

    public void LoadHpBar()
    {
      hpBar = Managers.Entity.Get<UEHpBar>(GetPos(), x => x.Init(curHp, maxHp));
    }

    private void OnDestroy()
    {
      EntityManager.Instance.onGetAfter -= OnGetEntityEntity;
      EntityManager.Instance.onReleaseBefore -= OnReleasedEntity;
    }
  }
}
