using System;
using System.Collections;
using System.Collections.Generic;
using Object.Element;
using UnityEngine;
using Utils;

namespace Object.Entity.Fighter
{
  public abstract class FighterController : MonoBehaviour
  {
    public Status status;

    public ElementType element;

    public ElementType hitElement = ElementType.None;

    public ElementData data;

    protected SpriteRenderer sr;

    private Coroutine colorCoroutine;
    private Coroutine checkHBCoroutine;

    public abstract void Attack();


    private void Awake()
    {
      sr = GetComponent<SpriteRenderer>();
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
  }
}