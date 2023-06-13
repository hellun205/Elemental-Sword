using System;
using System.Collections;
using System.Collections.Generic;
using Animation;
using Element;
using Manager;
using Object.Pool;
using UnityEngine;
using Utils;

namespace Object.Entity.Fighter
{
  [RequireComponent(typeof(HpBar))]
  public abstract class FighterController : Entity
  {
    public Status status;

    [SerializeField]
    protected SpriteRenderer sr;

    private SmoothColor colorAnim;
    private Coroutiner checkHbCoroutiner;
    private Rigidbody2D rb;

    public SingleElement damagedElement;

    public State state = State.None;

    public Dictionary<State, StateCoroutiner> stateCoroutines;

    private Coroutiner burningCoroutine;

    public abstract void Attack();

    protected HpBar hpBar;

    protected override void Awake()
    {
      base.Awake();
      // sr = GetComponent<SpriteRenderer>();
      rb = GetComponent<Rigidbody2D>();
      hpBar = GetComponent<HpBar>();

      stateCoroutines = new()
      {
        { State.Burning, new(new Coroutiner(this, BurningCoroutine), 0f) },
        { State.Slow, new(new Coroutiner(this, SlowCoroutine), 0f) },
        { State.Stun, new(new Coroutiner(this, StunCoroutine), 5f) },
      };
      burningCoroutine = new(this, Burning);

      colorAnim = new(this, new(() => sr.color, value => sr.color = value));
    }

    public override void OnGet()
    {
      base.OnGet();
      hpBar.maxHp = status.maxHp;
      hpBar.curHp = status.hp;
      hpBar.Init();
    }

    public override void OnReleased()
    {
      base.OnReleased();
      hpBar.Release();
    }

    // Todo: on release -> stop all coroutine.

    public virtual void Hit(float damage)
    {
      status.hp -= damage;
      hpBar.curHp = status.hp;
      hpBar.maxHp = status.maxHp;
      colorAnim.Start(Color.red, Color.white, 3f);
    }

    public virtual void Hit(float damage, SingleElement element, FighterController attacker, bool knockBack = false)
    {
      Hit(damage);
      Managers.Element.ApplyPassive(attacker, this, element);
      if (knockBack)
        rb.AddForce((position.x < attacker.position.x ? Vector2.left : Vector2.right) * 300f);
    }

    public void CheckHitBoxRepeat
    (
      Vector2 boxPos,
      Vector2 boxSize,
      Action<FighterController> callback,
      byte limit
    )
    {
      checkHbCoroutiner = new(this, () => CheckHitBoxCRT(boxPos, boxSize, callback, limit));
      checkHbCoroutiner.Start();
    }

    protected IEnumerator CheckHitBoxCRT
    (
      Vector2 boxPos,
      Vector2 boxSize,
      Action<FighterController> callback,
      byte limit
    )
    {
      var list = new List<FighterController>();
      while (list.Count < limit)
      {
        yield return new WaitForEndOfFrame();
        var colliders = Physics2D.OverlapBoxAll(boxPos, boxSize, 0);

        foreach (var target in colliders)
        {
          if
          (
            target.TryGetComponent(out FighterController component) &&
            !list.Contains(component) &&
            component != this &&
            !target.isTrigger
          )
          {
            list.Add(component);
            callback.Invoke(component);
          }
        }
      }
    }

    protected FighterController[] CheckHitBox(Vector2 boxPos, Vector2 boxSize, byte limit = byte.MaxValue)
    {
      var colliders = Physics2D.OverlapBoxAll(boxPos, boxSize, 0);
      var list = new List<FighterController>();
      foreach (var target in colliders)
      {
        if (target.TryGetComponent(out FighterController component) && component != this && !target.isTrigger)
          list.Add(component);
        if (list.Count >= limit) break;
      }

      return list.ToArray();
    }

    protected void AttackBox
    (
      float damage,
      SingleElement element,
      Transform pos,
      Vector2 boxPos,
      Vector2 boxSize,
      byte limit = byte.MaxValue
    )
    {
      var hits = CheckHitBox((Vector2)pos.position + boxPos, boxSize, limit);
      foreach (var hit in hits)
      {
        hit.Hit(damage);
        Managers.Element.ApplyPassive(this, hit, element);
      }
    }

    protected void AttackBoxRepeat
    (
      float damage,
      SingleElement element,
      Transform pos,
      Vector2 boxPos,
      Vector2 boxSize,
      byte limit = byte.MaxValue
    )
      => CheckHitBoxRepeat((Vector2)pos.position + boxPos, boxSize, opponent =>
      {
        opponent.Hit(damage);
        Managers.Element.ApplyPassive(this, opponent, element);
      }, limit);

    #region State

    public void AddState(State stateToAdd, float duration)
    {
      foreach (State _state in Enum.GetValues(typeof(State)))
      {
        if (_state == State.None || (stateToAdd & _state) == 0) continue;

        state |= _state;
        stateCoroutines[_state].duration = duration;
        stateCoroutines[_state].isActive = true;
        stateCoroutines[_state].coroutine.Start();
      }
    }

    public void StopState(State stateToStop)
    {
      foreach (State _state in Enum.GetValues(typeof(State)))
      {
        if (_state == State.None || (stateToStop & _state) == 0) continue;

        stateCoroutines[_state].isActive = false;
      }
    }

    public bool HasState(State _state) => (state & _state) != 0;

    private bool CheckEndTime(State _state, float timer) =>
      stateCoroutines[_state].duration > timer && stateCoroutines[_state].isActive;

    private IEnumerator SlowCoroutine()
    {
      const State thisState = State.Slow;
      var timer = 0f;

      if (!status.isSlowed)
      {
        status.isSlowed = true;
        status.SlowedValue = status.moveSpeed * Status.SlowPercent;
        status.moveSpeed -= status.SlowedValue;
      }

      while (CheckEndTime(thisState, timer))
      {
        yield return new WaitForEndOfFrame();
        timer += Time.deltaTime;
      }

      state &= ~thisState;
      status.moveSpeed += status.SlowedValue;
      status.isSlowed = false;
    }


    private IEnumerator BurningCoroutine()
    {
      const State thisState = State.Burning;
      var timer = 0f;

      if (!status.isBurning)
        burningCoroutine.Start();

      while (CheckEndTime(thisState, timer))
      {
        yield return new WaitForEndOfFrame();
        timer += Time.deltaTime;
      }

      status.isBurning = false;
      status.isFastBurning = false;
      status.isStrongBurn = false;
      state &= ~thisState;
    }

    private IEnumerator Burning()
    {
      status.isBurning = true;
      while (status.isBurning)
      {
        yield return new WaitForSeconds(status.burningSpeed);
        Hit(status.burningDamage);
      }
    }

    private IEnumerator StunCoroutine()
    {
      const State thisState = State.Stun;
      var timer = 0f;

      while (CheckEndTime(thisState, timer))
      {
        yield return new WaitForEndOfFrame();
        timer += Time.deltaTime;
      }

      state &= ~thisState;
    }

    #endregion
  }
}