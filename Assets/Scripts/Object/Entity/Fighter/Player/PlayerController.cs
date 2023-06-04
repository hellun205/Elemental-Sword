using UnityEngine;

namespace Object.Entity.Fighter.Player {
  public class PlayerController : FighterController {
    private Rigidbody2D rigid;
    
    private Animator anim;
    
    public override void Attack()
    {
      throw new System.NotImplementedException();
    }

    private void Awake() {
      rigid = GetComponent<Rigidbody2D>();
      anim = GetComponent<Animator>();
      
    }

    private void Update() {
      TestAttack();
    }

    private void TestAttack() {
      if (Input.GetKeyDown(KeyCode.LeftShift)) {
        anim.SetTrigger("attack");
      }
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