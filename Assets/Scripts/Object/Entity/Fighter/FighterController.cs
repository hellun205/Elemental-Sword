using Object.Sword;
using UnityEngine;

namespace Object.Entity.Fighter
{
  public abstract class FighterController : MonoBehaviour
  {
    public Status status { get; set; }

    public Element? firstElement;

    public Element secondElement;

    public abstract void Attack();

    public void Damage()
    {
      
    }
  }
} 