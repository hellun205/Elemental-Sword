using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Object.Pool {
  public class PoolManager : MonoBehaviour {
    private static PoolManager Instance { get; set; }

    private Dictionary<PoolType, IObjectPool<PoolManagement>> pool;

    public Transform container;


    private void Awake() {
      if (Instance == null) Instance = this;
      Destroy(this);

      pool = new Dictionary<PoolType, IObjectPool<PoolManagement>>();

      foreach (PoolType type in Enum.GetValues(typeof(PoolType))) {
        pool.Add(type,
          new ObjectPool<PoolManagement>(() => OnCreatePool(type), OnGetPool, OnReleasePool, OnDestroyPool));
      }
    }

    private PoolManagement OnCreatePool(PoolType type) {
      var prefab = PrefabManager.Get(type.ToString());
      var obj = Instantiate(prefab, container).GetComponent<PoolManagement>();
      obj.type = type;
      return obj;
    }

    private void OnGetPool(PoolManagement obj) => obj.gameObject.SetActive(true);

    private void OnReleasePool(PoolManagement obj) => obj.gameObject.SetActive(false);

    private void OnDestroyPool(PoolManagement obj) => Destroy(obj.gameObject);

    public static PoolManagement Get(PoolType type) => Instance.pool[type].Get();

    public static PoolManagement Get(PoolType type, Vector2 position) {
      var obj = Get(type);
      obj.position = position;
      return obj;
    }
    
    public static void Release(PoolManagement obj) => Instance.pool[obj.type].Release(obj);
  }
}