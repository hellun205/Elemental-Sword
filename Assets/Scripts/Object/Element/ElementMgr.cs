using System;
using System.Linq;
using Manager;
using UnityEngine;
using Utils;

namespace Object.Element
{
  public class ElementMgr : SingleTon<ElementMgr>, IDontDestroy
  {
    public ElementSetting setting;

    public BaseElement[] elements;

    public T GetElement<T>() where T : BaseElement
    {
      var type = typeof(T).GetTypeProperty<ElementType>();
      return (T)elements.Single(element => element.type == type);
    }

    public ElementSet GetSetting(ElementType sender, ElementType target)
      => setting.setting[sender].Single(set => set.elementType == target);
  }
}