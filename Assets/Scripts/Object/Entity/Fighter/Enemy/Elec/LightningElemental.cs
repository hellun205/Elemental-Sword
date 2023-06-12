﻿using System;
using System.Collections;
using Manager;
using Unity.VisualScripting;
using UnityEngine;
using Utils;

namespace Object.Entity.Fighter.Enemy.Elec
{
  public class LightningElemental : EnemyController
  {
    [SerializeField]
    private Animator thisAnimator;

    private Animator cloudAnimator;

    private Coroutiner attackCoroutiner;
    private Coroutiner attackLoopCoroutiner;

    [SerializeField]
    private GameObject darkCloud;

    [SerializeField]
    private BoxCollider2D atkCollider;
    
    [SerializeField]
    private CircleCollider2D followCollider;

    private Vector2 colPos;
    private Vector2 colSize;

    private bool isAttacking;

    [SerializeField]
    private float attackSpeed = .2f;
    
    [SerializeField]
    private float attackCooldown = 2f;

    private bool isFollowing = false;

    [SerializeField]
    private string[] atkAudios;

    [SerializeField]
    private float cloudDistance = 4f;

    protected override void Awake()
    {
      base.Awake();
      cloudAnimator = darkCloud.GetComponent<Animator>();
      attackCoroutiner = new Coroutiner(this, AtkCRT);
      attackLoopCoroutiner = new Coroutiner(this, AtkLoopCRT);
      colPos = atkCollider.offset;
      colSize = atkCollider.size;

      darkCloud.transform.parent = Entity.container;
    }

    private void Update()
    {
      if (status.hp < status.maxHp * 0.3f)
        attackCooldown = 0.85f;  
      else if (status.hp < status.maxHp * 0.6f)
        attackCooldown = 1f;
      
      if (!isFollowing) return;
      var c = Physics2D.OverlapCircleAll(transform.position, followCollider.radius, LayerMask.GetMask("Player"));
      if (c.Length == 0)
        movement.Move(Managers.Player.position.x < position.x ? Direction.Left : Direction.Right);
      else
        movement.Move(Direction.None);
    }

    public override void Attack()
    {
      attackCoroutiner.Start();
    }

    private IEnumerator AtkCRT()
    {
      isAttacking = true;
      var pos = Managers.Player.position.Plus(y: cloudDistance);
      darkCloud.transform.position = pos;
      cloudAnimator.SetTrigger("start");
      yield return new WaitForSeconds(attackSpeed);
      cloudAnimator.SetTrigger("attack");
      Managers.Audio.PlaySFX(atkAudios.Random());
      yield return new WaitForSeconds(0.1f);
      AttackBox(status.damage, element, darkCloud.transform, colPos, colSize);
      isAttacking = false;
    }

    private IEnumerator AtkLoopCRT()
    {
      while (true)
      {
        yield return new WaitUntil(() => !isAttacking);
        yield return new WaitForSeconds(attackCooldown);
        Attack();
      }
    }

    public override void OnGet()
    {
      base.OnGet();
      darkCloud.SetActive(true);
    }

    public override void OnReleased()
    {
      base.OnReleased();
      attackLoopCoroutiner.Stop();
      darkCloud.SetActive(false);
    }

    private void OnDrawGizmos()
    {
      Gizmos.DrawWireCube(darkCloud.transform.position + (Vector3)colPos, colSize);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
      if (col.CompareTag("Player"))
      {
        attackLoopCoroutiner.Start();
        isFollowing = true;
      }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
      if (other.CompareTag("Player"))
      {
        attackLoopCoroutiner.Stop();
        isFollowing = false;
        movement.Move(Direction.None);
      }
    }

    private void OnCollisionStay2D(Collision2D col)
    {
      if (col.transform.CompareTag("Player"))
      {
        Managers.Player.Hit(status.damage, element, this, true);
      }
    }
  }
}
