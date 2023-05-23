using System;
using System.Collections;
using Object.Pool;
using UnityEngine;

namespace Object.Entity.Player {
  public class PlayerController : MonoBehaviour {
    public static PlayerController Instance { get; private set; }
    private Rigidbody2D rigid;
    private Animator anim;

    [Header("Movement")]
    public float moveSpeed = 3f;

    public float jumpSpeed = 5f;

    public KeyCode jumpKey = KeyCode.Space;

    public LayerMask floorMask;

    private bool isJumping = false;

    private bool canFlip = true;

    private bool wasRight = true;

    private float floorDistance;

    private void Awake() {
      if (Instance == null) Instance = this;
      else Destroy(gameObject);
      DontDestroyOnLoad(gameObject);
      rigid = GetComponent<Rigidbody2D>();
      anim = GetComponent<Animator>();
      
      floorDistance = GetComponent<BoxCollider2D>().bounds.extents.y + 0.1f;
    }

    private void FixedUpdate() {
      Move();
    }

    private void Update() {
      Jump();
      CheckGround();
      TestAttack();
    }

    #region Movement

    private void Move() {
      var horizontal = Input.GetAxisRaw("Horizontal");
      // anim.SetBool("isWalking", horizontal != 0);
      transform.Translate(horizontal * Time.fixedDeltaTime * moveSpeed, 0f, 0f);
      if (horizontal > 0) wasRight = true;
      else if (horizontal < 0) wasRight = false;
      
      Flip();
    }

    private void Jump() {
      if (!isJumping && Input.GetKey(jumpKey)) {
        SetJump(true);
        // rb.AddForce(Vector2.up * (jumpSpeed * 100f));
        rigid.velocity = Vector2.up * jumpSpeed;
      }
    }
    
    private void CheckGround() {
      if (rigid.velocity.y < 0) {
        var hit = Physics2D.Raycast(transform.position, Vector2.down, floorDistance, floorMask);

        if (hit && hit.transform.CompareTag("Ground")) {
          SetJump(false);
        }
      }
    }
    
    private void SetJump(bool value) {
      isJumping = value;
      // anim.SetBool("isJumping", value);
    }

    private void Flip() {
      if (!canFlip) return;
      if (wasRight) {
        transform.localScale = new Vector3(1f, 1f, 1f);
      } else {
        transform.localScale = new Vector3(-1f, 1f, 1f);
      }
    }

    #endregion

    private void TestAttack() {
      if (Input.GetKeyDown(KeyCode.LeftShift)) {
        anim.SetTrigger("attack");
      }
    }

    private void Start() {
      // StartCoroutine(SpawnCoroutine());
      PoolManager.Get(PoolType.Enemy, new Vector2(0, 5));
    }

    private IEnumerator SpawnCoroutine() {
      while (true) {
        PoolManager.Get(PoolType.Enemy, new Vector2(0, 5));
        yield return new WaitForSeconds(5f);
      }
    }
  }
}