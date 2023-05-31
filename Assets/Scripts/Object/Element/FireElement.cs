using System;
using System.Collections;
using Object.Entity.Fighter;
using UnityEngine;

namespace Object.Element
{
  [CreateAssetMenu(fileName = "Fire", menuName = "Element/Fire", order = 0)]
  public sealed class FireElement : BaseElement
  {
    public static ElementType Type => ElementType.Fire;

    public float dotDamage;

    public float dotSpeed;

    public float time;

    private void Awake()
    {
      type = Type;
    }

    public override void Attack(FighterController opponent)
    {
      base.Attack(opponent);
      var set = GetElementSet(opponent);
      // todo : flame effect
      
      // dot damage
      if (opponent.data.dotCoroutine is not null)
        opponent.StopCoroutine(opponent.data.dotCoroutine);
      opponent.data.dotCoroutine = opponent.StartCoroutine(DotCoroutine(opponent, set));
    }

    private IEnumerator DotCoroutine(FighterController opponent, ElementSet set)
    {
      var res = 1 - set.resistance;
      var armor = 1 - set.armor;

      opponent.data.curDotCount = 0;
      opponent.data.dotDmg = dotDamage * armor;
      opponent.data.maxDotCount = (byte)Mathf.RoundToInt(time * res);
      opponent.data.dotSpeed = dotSpeed * res;
      while (opponent.data.maxDotCount > opponent.data.curDotCount)
      {
        yield return new WaitForSeconds(opponent.data.dotSpeed);
        opponent.Hit(opponent.data.dotDmg);
        opponent.data.curDotCount++;
      }
      
      
    }
    
    // private 
  }
}