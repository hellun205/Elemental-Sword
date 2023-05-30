using System.Collections;
using Object.Entity.Fighter.Enemy;
using Object.Pool;
using UnityEngine;

namespace Object.Entity.Fighter.Player {
  public class PlayerController : MonoBehaviour {
    public static PlayerController Instance { get; private set; }
    private Rigidbody2D rigid;
    private Animator anim;
    

    private void Awake() {
      if (Instance == null) Instance = this;
      else Destroy(gameObject);
      DontDestroyOnLoad(gameObject);
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

    private void Start() {
      // StartCoroutine(SpawnCoroutine());
      // PoolManager.Get(PoolType.Enemy_Frog, new Vector2(0, 5));
      PoolManager.Get<Frog>(new Vector2(0f, 5f));
    }
    

    private IEnumerator SpawnCoroutine() {
      while (true) {
        PoolManager.Get(PoolType.Enemy_Frog, new Vector2(0, 5));
        yield return new WaitForSeconds(5f);
      }
    }
  }
}