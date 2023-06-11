using Element;
using UnityEngine;

namespace Object.Entity.Fighter.Enemy
{
  public abstract class EnemyController : FighterController
  {
    public SingleElement element;
    
    [SerializeField]
    protected Movement movement;

    protected override void Awake()
    {
      base.Awake();
      
    }
  }
}