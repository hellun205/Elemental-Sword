using System.Collections.Generic;
using Manager;
using Object.Entity.Fighter;
using Object.Entity.Fighter.Player;
using UnityEngine;

namespace Element
{
  public class ElementManager : SingleTon<ElementManager>
  {
    public delegate void PassiveAtk(FighterController attacker, FighterController target);

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
      if (element == SingleElement.None)
        return;
      elementSetting[element].passive.Invoke(attacker, target);
      target.damagedElement = element;
    }

    private static void FirePassive(FighterController attacker, FighterController target)
    {
      // Overlap Passive
      // Todo
      const float burningDuration = 5f;

      switch (target.damagedElement)
      {
        // 풀 -> 불
        case SingleElement.Grass:
        {
          target.status.isStrongBurn = true;
          break;
        }

        // 물 -> 불
        case SingleElement.Water:
        {
          target.StopState(State.Slow);
          break;
        }
      }

      // Normal Passive
      target.AddState(State.Burning, burningDuration);
    }

    private static void WaterPassive(FighterController attacker, FighterController target)
    {
      const float slowDuration = 2f;
      // Overlap Passive
      // Todo
      // 불 -> 물
      if (target.HasState(State.Burning))
      {
        target.StopState(State.Burning);
      }

      switch (target.damagedElement)
      {
        // 불 -> 물
        case SingleElement.Fire:
        {
          target.status.isBurning = false;
          break;
        }

        // 풀 -> 물
        case SingleElement.Grass:
        {
          target.status.HealPercent(3f);
          break;
        }
      }

      // Normal Passive
      target.AddState(State.Slow, slowDuration);
    }

    private static void GrassPassive(FighterController attacker, FighterController target)
    {
      // Overlap Passive
      // Todo
      switch (target.damagedElement)
      {
        // 불 -> 풀
        case SingleElement.Fire:
        {
          target.stateCoroutines[State.Burning].duration += 5f;
          break;
        }
      }

      // Normal Passive
      attacker.status.HealPercent(0.07f);
    }

    private static void LandPassive(FighterController attacker, FighterController target)
    {
      // Overlap Passive
      // Todo
      switch (target.damagedElement)
      {
        // 불 -> 땅
        case SingleElement.Fire:
        {
          target.stateCoroutines[State.Burning].duration -= 3f;
          break;
        }
      }

      // Normal Passive
      // ToDo
    }

    private static void ElectricityPassive(FighterController attacker, FighterController target)
    {
      const float stunDuration = 1f;
      // Overlap Passive
      // Todo
      switch (target.damagedElement)
      {
      }

      // Normal Passive
      if (attacker is PlayerController)
      {
        // Todo
      }
      else
        target.AddState(State.Stun, stunDuration);
    }

    #endregion
  }
}
