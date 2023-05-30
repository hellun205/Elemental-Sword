using System.Collections.Generic;
using System.Linq;
using Manager;
using UnityEngine;

namespace Object {
  public class PrefabManager : SingleTon<PrefabManager>, IDontDestroy {
    public List<GameObject> prefabs;

    public static GameObject Get(string name) => instance.prefabs.Single(obj => obj.name == name);
  }
}