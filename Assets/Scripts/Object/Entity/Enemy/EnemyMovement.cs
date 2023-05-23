using System;
using System.Collections;
using Object.Entity.Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Object.Entity.Enemy {
  public class EnemyMovement : Movement {
    [Header("Move Logic - enemy")]
    [SerializeField]
    private bool moveLeft = true;

    [SerializeField]
    private bool moveRight = true;

    [SerializeField]
    [Range(0.5f, 5f)]
    private float thinkTime = 3f;

    [SerializeField]
    private bool checkPlayer = false;

    [SerializeField]
    [Range(0.5f, 7f)]
    private float checkPlayerDistance = 3f;

    [SerializeField]
    [HideInInspector]
    private bool isFollowingPlayer = false;

    [SerializeField]
    [Range(1f, 5f)]
    private float followingPlayerSpeed = 2f;

    [SerializeField]
    private float playerDistance = 0.5f;
    
    [SerializeField]
    private bool checkGround = true;

    private float checkGDistanceX;
    private float checkGDistanceY;

    [SerializeField]
    private bool jumping;

    protected override void Awake() {
      base.Awake();
      var bounds = collider.bounds;
      checkGDistanceX = bounds.extents.x + 0.1f;
      checkGDistanceY = bounds.extents.y;
    }

    private void Start() {
      StartCoroutine(Walk());
    }

    protected override void Update() {
      base.Update();
      if (checkGround) {
        var dirH = (int)currentDirection;
        var pos = GetColliderCenter();
        pos.x += checkGDistanceX * dirH;
        pos.y += checkGDistanceY;
        var hit = Physics2D.Raycast(pos, Vector2.down, 1f);
        Debug.DrawRay(pos, Vector3.down * 1f, Color.red);
        if (!hit) Move(0f);
      }
      if (checkPlayer && !isFollowingPlayer) {
        var dirH = (int)currentDirection;
        var position = transform.position;
        var hit = Physics2D.Raycast(position, Vector2.right * dirH, checkPlayerDistance);
        Debug.DrawRay(position, Vector2.right * (dirH * checkPlayerDistance), Color.cyan);
        if (hit && hit.transform.CompareTag("Player")) {
          isFollowingPlayer = true;
          StopCoroutine(Walk());
          moveSpeed += followingPlayerSpeed;
        }
      }
      if (isFollowingPlayer) {
        var playerPos = PlayerController.Instance.transform.position;
        var thisPos = transform.position;
        var dir = thisPos.x > playerPos.x ? -1f : 1f;

        var rayPos = GetColliderCenter();
        rayPos.x += collider.bounds.extents.x * (int)currentDirection;
        var hit = Physics2D.Raycast(rayPos, Vector2.right * (int)currentDirection, playerDistance);
        Debug.DrawRay(rayPos, Vector2.right * ((int)currentDirection * playerDistance));
        Move(hit && hit.transform.CompareTag("Player") ? 0f : dir);
      }
    }

    private IEnumerator Walk() {
      while (true) {
        var dir = Random.Range(moveLeft ? -1 : 0, moveRight ? 2 : 1);
        Move(dir);
        yield return new WaitForSeconds(thinkTime);

        var jump = Random.Range(0, 2);
        if (jump == 1) Jump();
      }
    }


  }
}