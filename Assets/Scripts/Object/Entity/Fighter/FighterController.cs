using Object.Sword;
using UnityEngine;

namespace Object.Entity.Fighter
{
  public abstract class FighterController : MonoBehaviour
  {
    public Status status;

    public Element hitElement;

    public abstract void Attack();

    public void Damage()
    {
      
    }
  }
} 