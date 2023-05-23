using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Object {
  public class PrefabManager : MonoBehaviour {
    public static PrefabManager Instance { get; private set; }

    public List<GameObject> prefabs;

    private void Awake() {
      if (Instance == null) Instance = this;
      Destroy(this);
    }

    public static GameObject Get(string name) => Instance.prefabs.Single(obj => obj.name == name);

  }
}