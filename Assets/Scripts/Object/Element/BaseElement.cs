using Object.Entity.Fighter;
using UnityEngine;

namespace Object.Element
{
  public abstract class BaseElement : ScriptableObject
  {
    public ElementType type;

    public float damage;

    public virtual void Attack(FighterController opponent)
    {
      var set = GetElementSet(opponent);
      var dmg = CalculateDamage(damage, set);
      opponent.Hit(dmg);
    }
    
    protected ElementSet GetElementSet(FighterController opponent)
    => ElementMgr.instance.setting.setting[type].Get(opponent.element);
    
    protected float CalculateDamage(float dmg, ElementSet set)
    => (damage/** * set.damage*/) * (1 - set.resistance);
  }
}