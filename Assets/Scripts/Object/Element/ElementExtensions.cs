using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Object.Element
{
  public static class ElementExtensions
  {
    public static string GetText(this ElementType elementType) => elementType switch
    {
      ElementType.Electricity => "전기",
      ElementType.Fire => "불",
      ElementType.Grass => "풀",
      ElementType.Land => "땅",
      ElementType.Water => "물",
      _ => null
    };

    public static ElementSet Get(this IEnumerable<ElementSet> elementSet, ElementType elementType)
      // => elementSet.Single(set => set.elementType == elementType);
    {
      Debug.Log(elementType);
      return new ElementSet();
    }
  }
}