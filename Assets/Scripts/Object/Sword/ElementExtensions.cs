using System.Collections.Generic;
using System.Linq;

namespace Object.Sword
{
  public static class ElementExtensions
  {
    public static string GetText(this Element element) => element switch
    {
      Element.Electricity => "전기",
      Element.Fire => "불",
      Element.Grass => "풀",
      Element.Land => "땅",
      Element.Water => "물",
      _ => null
    };

    public static ElementSet Get(this IEnumerable<ElementSet> elementSet, Element element)
      => elementSet.Single(set => set.element == element);
  }
}