using System;
using Manager;
using Object.Pool;
using UnityEngine;

namespace Object.Entity
{
  public class HpBar : MonoBehaviour
  {
    [SerializeField]
    private Entity entity;

    private UEHpBar hpBar;

    public float curHp;
    public float maxHp;

    [SerializeField]
    private Collider2D col;

    [SerializeField]
    private float distance = 0.1f;

    private float colDistance;

    private void Reset()
    {
      entity = GetComponent<Entity>();
    }

    private Vector2 GetPos() => new Vector2(
      entity.position.x,
      entity.position.y + colDistance + distance
    );

    private void Awake()
    {
      colDistance = col.bounds.extents.y;
    }

    private void Update()
    {
      hpBar.position = GetPos();
      hpBar.MaxValue = maxHp;
      hpBar.Value = curHp;
    }

    public void Init()
    {
      LoadHpBar();
    }

    public void Release()
    {
      hpBar.Release();
    }

    public void LoadHpBar()
    {
      hpBar = Managers.Entity.Get<UEHpBar>(GetPos(), x => x.Init(curHp, maxHp));
    }
  }
}