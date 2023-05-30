using Object.Entity.Fighter.Enemy;
using UnityEngine;

namespace Object.Sword
{
  [CreateAssetMenu(fileName = "BaseSword", menuName = "Sword/Base", order = 0)]
  public abstract class BaseSword : ScriptableObject
  {
    public Element element;

    public abstract void OnHit(EnemyController enemy);
    
  }
}