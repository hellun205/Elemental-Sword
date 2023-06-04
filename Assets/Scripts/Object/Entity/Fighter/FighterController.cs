﻿using System;
using System.Collections;
using System.Collections.Generic;
using Element;
using UnityEngine;
using Utils;

namespace Object.Entity.Fighter
{
  public abstract class FighterController : MonoBehaviour
  {
    public Status status;

    protected SpriteRenderer sr;

    private Coroutine colorCoroutine;
    private Coroutine checkHBCoroutine;

    public SingleElement damagedElement;

    public State state = State.None;

    private Dictionary<State, StateCoroutiner> stateCoroutines;

    public abstract void Attack();

    private void Awake()
    {
      sr = GetComponent<SpriteRenderer>();

      stateCoroutines = new()
      {
        { State.Burning, new(new Coroutiner(this, BurningCoroutine), 0f) },
        { State.Slow, new(new Coroutiner(this, SlowCoroutine), 0f) },
        { State.Stun, new(new Coroutiner(this, StunCoroutine), 0f) },
      };
    }


    public void Hit(float damage)
    {
      status.hp -= damage;
      var tmpColor = sr.color;
      ChangeColor(Color.red);
      ChangeColorSmooth(tmpColor, 3f);
    }

    public void ChangeColorSmooth(Color color, float smoothing = 3f)
      => this.StopNStartCoroutine(ref colorCoroutine, ChangeColorSmoothCRT(color, smoothing));

    public void ChangeColor(Color color) => sr.color = color;


    private IEnumerator ChangeColorSmoothCRT(Color color, float smoothing)
    {
      while (sr.color != color)
      {
        sr.color = Color.Lerp(sr.color, color, Time.deltaTime * smoothing);
        yield return new WaitForEndOfFrame();
      }
    }

    public void CheckHitBoxRepeat
    (
      Transform boxTransform,
      Vector2 boxSize,
      Action<FighterController> callback,
      byte limit
    )
      => this.StopNStartCoroutine(ref checkHBCoroutine, CheckHitBoxCRT(boxTransform, boxSize, callback, limit));

    public void StopCheckHitBox() => StopCoroutine(checkHBCoroutine);


    protected IEnumerator CheckHitBoxCRT
    (
      Transform boxTransform,
      Vector2 boxSize,
      Action<FighterController> callback,
      byte limit
    )
    {
      var list = new List<FighterController>();
      while (list.Count < limit)
      {
        yield return new WaitForEndOfFrame();
        var colliders = Physics2D.OverlapBoxAll(boxTransform.position, boxSize, 0);

        foreach (var target in colliders)
        {
          if (target.TryGetComponent(out FighterController component) &&
              !list.Contains(component) &&
              component != this)
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
        if (target.TryGetComponent(out FighterController component) && component != this)
          list.Add(component);
        if (list.Count <= limit) break;
      }

      return list.ToArray();
    }

    #region State

    public void AddState(State stateToAdd, float duration)
    {
      foreach (State _state in Enum.GetValues(typeof(State)))
      {
        if (_state == State.None) continue;
        if ((stateToAdd & _state) != 0)
        {
          stateCoroutines[_state].duration = duration;
          stateCoroutines[_state].coroutine.Start();
        }
      }
    }

    private IEnumerator SlowCoroutine()
    {
      const State thisState = State.Slow;
      state |= thisState;
      
      if (!status.isSlowed)
      {
        status.isSlowed = true;
        status.SlowedValue = status.moveSpeed * Status.SlowPercent;
        status.moveSpeed -= status.SlowedValue;
      }

      yield return new WaitForSeconds(stateCoroutines[thisState].duration);
      StopSlow();
    }

    private void StopSlow()
    {
      state &= ~State.Slow;
      if (status.isSlowed)
        status.moveSpeed += status.SlowedValue;
      status.isSlowed = false;
    }

    private IEnumerator BurningCoroutine()
    {
      const State thisState = State.Burning;
      state |= thisState;
      
      throw new NotImplementedException();
      yield return new WaitForSeconds(stateCoroutines[thisState].duration);
      StopBurning();
    }
    
    private void StopBurning()
    {
      state &= ~State.Burning;
    }

    private IEnumerator StunCoroutine()
    {
      const State thisState = State.Stun;
      state |= thisState;
      
      yield return new WaitForSeconds(stateCoroutines[thisState].duration);
      StopStun();
    }

    private void StopStun()
    {
      state &= ~State.Stun;
    }

    #endregion
  }
}
