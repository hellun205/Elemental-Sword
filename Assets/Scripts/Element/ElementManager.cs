using System.Collections.Generic;
using Manager;
using Object.Entity.Fighter;
using UnityEngine;

namespace Element
{
  public class ElementManager : SingleTon<ElementManager>
  {
    public delegate void ElementAttack();

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

    private static void FirePassive(FighterController attacker, FighterController target)
    {
      switch (target.damagedElement)
      {
        default:
        {
          
          break;
        }
      }
    }

    private static void WaterPassive(FighterController attacker, FighterController target)
    {
      throw new System.NotImplementedException();
    }

    private static void GrassPassive(FighterController attacker, FighterController target)
    {
      throw new System.NotImplementedException();
    }

    private static void LandPassive(FighterController attacker, FighterController target)
    {
      throw new System.NotImplementedException();
    }

    private static void ElectricityPassive(FighterController attacker, FighterController target)
    {
      throw new System.NotImplementedException();
    }

    #endregion
  }
}
