using System.Collections.Generic;
using System.Linq;
using Manager;
using UnityEngine;

namespace Object {
  public class PrefabManager : GameObjectSingleTon<PrefabManager>, IDontDestroy {
    public List<GameObject> prefabs;

    public GameObject Get(string name) => prefabs.Single(obj => obj.name == name);
  }
}