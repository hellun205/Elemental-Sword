using System.Collections.Generic;
using Manager;
using Object.Entity.Fighter;
using Object.Entity.Fighter.Player;
using UnityEngine;

namespace Element
{
  public class ElementManager : SingleTon<ElementManager>
  {
    public delegate void ElementAttack();

    public delegate void PassiveAtk(FighterController attacker, FighterController target);

    public const float SlowDuration = 2f;
    public const float StunDuration = 1f;

    public readonly Dictionary<SingleElement, ElementInfo> elementSetting = new()
    {
      {
        SingleElement.Fire, new()
        {
          color = Color.red,
          passive = FirePassive,
        }
      },
      {
        SingleElement.Water, new()
        {
          color = Color.blue,
          passive = WaterPassive,
        }
      },
      {
        SingleElement.Grass, new()
        {
          color = Color.green,
          passive = GrassPassive,
        }
      },
      {
        SingleElement.Land, new()
        {
          color = Color.grey,
          passive = LandPassive,
        }
      },
      {
        SingleElement.Electricity, new()
        {
          color = Color.yellow,
          passive = ElectricityPassive,
        }
      },
    };

    #region Passive Attacks

    public void ApplyPassive(FighterController attacker, FighterController target, SingleElement element)
    {
      elementSetting[element].passive.Invoke(attacker, target);
      target.damagedElement = element;
    }

    private static void FirePassive(FighterController attacker, FighterController target)
    {
      // Overlap Passive
      const float burningDuration = 5f;
      
      switch (target.damagedElement)
      {
        default:
        {
          break;
        }
      }
      target.AddState(State.Burning, burningDuration);
    }

    private static void WaterPassive(FighterController attacker, FighterController target)
    {
      // Overlap Passive
      // 불 -> 물
      if (target.HasState(State.Burning))
      {
        target.StopState(State.Burning);
      }
      
      switch (target.damagedElement)
      {
        // 풀 -> 물
        case SingleElement.Grass:
        {
          attacker.status.HealPercent(3f);
          break;
        }
        
      }
      target.AddState(State.Slow, SlowDuration);
    }

    private static void GrassPassive(FighterController attacker, FighterController target)
    {
      // Overlap Passive
      switch (target.damagedElement)
      {
        default:
        {
          
          break;
        }
      }
      
      attacker.status.HealPercent(0.07f);
    }

    private static void LandPassive(FighterController attacker, FighterController target)
    {
      // Overlap Passive
      switch (target.damagedElement)
      {
        default:
        {
          break;
        }
      }
    }

    private static void ElectricityPassive(FighterController attacker, FighterController target)
    {
      // Overlap Passive
      switch (target.damagedElement)
      {
        default:
        {


          break;
        }
      }
      // Normal Passive
      if (attacker is PlayerController)
      {
            
      }
      else
        target.AddState(State.Stun, StunDuration);
    }

    #endregion
  }
}
