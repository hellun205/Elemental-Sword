using System.Collections;
using Element;
using UnityEngine;
using Utils;

namespace Object.Entity.Fighter.Player
{
  public class PlayerController : FighterController
  {
    private Rigidbody2D rigid;

    private bool canHit = true;

    private Coroutiner hitCoroutiner;

    private Animator anim;

    public override void Attack()
    {
      throw new System.NotImplementedException();
    }

    protected override void Awake()
    {
      base.Awake();
      rigid = GetComponent<Rigidbody2D>();
      anim = GetComponent<Animator>();
      hitCoroutiner = new Coroutiner(this, HitCRT);
    }

    private void Update()
    {
      TestAttack();
    }

    private void TestAttack()
    {
      if (Input.GetKeyDown(KeyCode.LeftShift))
      {
        anim.SetTrigger("attack");
      }
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
  }
}
