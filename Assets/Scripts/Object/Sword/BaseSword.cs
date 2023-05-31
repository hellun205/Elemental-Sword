using Object.Element;
using Object.Entity.Fighter;
using UnityEngine;

namespace Object.Sword
{
  // [CreateAssetMenu(fileName = "BaseSword", menuName = "Sword/Base", order = 0)]
  public abstract class BaseSword : ScriptableObject
  {
    public ElementType element;

    public abstract void OnHit(FighterController enemy);
    
  }
}