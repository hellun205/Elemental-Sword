using System;
using System.Collections;
using Dialogue;
using Element;
using Manager;
using UnityEngine;
using Utils;

namespace Object.Entity.Fighter.Player
{
  public class PlayerController : FighterController, ITalker
  {
    private Rigidbody2D rigid;

    private bool canHit = true;

    private Coroutiner hitCoroutiner;

    private Animator anim;

    [SerializeField]
    private BoxCollider2D atkBound;

    [SerializeField]
    private Sprite _avartar;

    public override void Attack()
    {
      AttackBox(status.damage, Managers.PlayerM.currentElement, transform, atkBound.offset, atkBound.size);
    }

    protected override void Awake()
    {
      base.Awake();
      rigid = GetComponent<Rigidbody2D>();
      anim = GetComponent<Animator>();
      hitCoroutiner = new Coroutiner(this, HitCRT);
    }

    private void Start()
    {
      hpBar.maxHp = status.maxHp;
      hpBar.curHp = status.hp;
      hpBar.Init();
    }

    private void Update()
    {
      if (Input.GetMouseButtonDown(0))
        Attack();
    }

    public override void Hit(float damage)
    {
      if (canHit)
      {
        base.Hit(damage);
        hitCoroutiner.Start();
      }
    }

    public override void Hit(float damage, SingleElement element, FighterController attacker, bool knockBack = false)
    {
      if (canHit)
        base.Hit(damage, element, attacker, knockBack);
    }

    private IEnumerator HitCRT()
    {
      canHit = false;
      yield return new WaitForSeconds(0.5f);
      canHit = true;
    }

    // private void Start() {
    //   // StartCoroutine(SpawnCoroutine());
    //   // PoolManager.Get(PoolType.Enemy_Frog, new Vector2(0, 5));
    //   PoolManager.Get<Frog>(new Vector2(0f, 5f));
    // }
    //
    //
    // private IEnumerator SpawnCoroutine() {
    //   while (true) {
    //     PoolManager.Get(PoolType.Enemy_Frog, new Vector2(0, 5));
    //     yield return new WaitForSeconds(5f);
    //   }
    // }
    public Sprite avartar => _avartar;
    public AvartarDirection avartarDirection => AvartarDirection.Left;
  }
}
