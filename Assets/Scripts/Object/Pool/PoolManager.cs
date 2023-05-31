using System;
using System.Collections.Generic;
using Manager;
using UnityEngine;
using UnityEngine.Pool;
using Utils;

namespace Object.Pool {
  public class PoolManager : SingleTon<PoolManager>, IDontDestroy {
    private Dictionary<PoolType, IObjectPool<PoolManagement>> pool;

    public Transform container;

    private Vector2 tempPosition;

    protected override void Awake() {
      base.Awake();
      pool = new Dictionary<PoolType, IObjectPool<PoolManagement>>();

      foreach (PoolType type in Enum.GetValues(typeof(PoolType))) {
        pool.Add(type,
          new ObjectPool<PoolManagement>(() => OnCreatePool(type), OnGetPool, OnReleasePool, OnDestroyPool));
      }
    }

    private PoolManagement OnCreatePool(PoolType type) {
      var prefab = PrefabManager.Get(type.ToString());
      var obj = Instantiate(prefab, container).GetComponent<PoolManagement>();
      return obj;
    }

    private void OnGetPool(PoolManagement obj)
    {
      obj.position = tempPosition;
      obj.gameObject.SetActive(true);
    }

    private void OnReleasePool(PoolManagement obj) => obj.gameObject.SetActive(false);

    private void OnDestroyPool(PoolManagement obj) => Destroy(obj.gameObject);

    public static PoolManagement Get(PoolType type, Vector2 position)
    {
      instance.tempPosition = position;
      return instance.pool[type].Get();
    }

    public static T Get<T>(Vector2 position) where T : PoolManagement
      => (T)Get(typeof(T).GetTypeProperty<PoolType>(), position);

    public static void Release(PoolManagement obj) => instance.pool[obj.type].Release(obj);
    
  }
}