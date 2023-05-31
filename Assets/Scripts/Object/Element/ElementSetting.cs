using UnityEngine;

namespace Object.Element
{
  [CreateAssetMenu(fileName = "ElementSetting", menuName = "Element/Setting", order = 0)]
  public class ElementSetting : ScriptableObject
  {
    public SerializableDictionary<ElementType, ElementSet[]> setting;
  }
}