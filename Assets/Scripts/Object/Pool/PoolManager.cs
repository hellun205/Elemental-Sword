using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Manager;
using UnityEngine;
using UnityEngine.Pool;

namespace Object.Pool
{
  public abstract class PoolManager<T, TObject> : GameObjectSingleTon<T> 
    where T : PoolManager<T, TObject>
    where TObject : PoolManagement
  {
    public delegate void PoolEventListener(TObject sender);

    public event Action onGetBefore;
    public event PoolEventListener onGetAfter;
    public event PoolEventListener onReleaseBefore;
    public event Action onReleaseAfter;

    protected Dictionary<string, IObjectPool<TObject>> pools = new Dictionary<string, IObjectPool<TObject>>();

    protected Transform parent;

    public TComponent Get<TComponent>([CanBeNull] Action<TComponent> objSet = null) where TComponent : Component
    {
      var type = typeof(TComponent).Name;
      if (!pools.ContainsKey(type))
      {
        pools.Add(type, new ObjectPool<TObject>(() => OnCreatePool(type), OnGetPool, OnReleasePool, OnDestroyPool));
      }

      onGetBefore?.Invoke();
      var obj = pools[type].Get();
      var objT = obj as TComponent;
      objSet?.Invoke(objT);
      onGetAfter?.Invoke(obj);
      return objT;
    }

    public void Release<T>(T obj) where T : Component
    {
      var type = typeof(T).Name;
      if (!pools.ContainsKey(type))
      {
        Debug.LogError($"Can't release object. This is not managed by this manager.");
        return;
      }

      var objT = obj as TObject;
      onReleaseBefore?.Invoke(objT);
      pools[type].Release(objT);
      onReleaseAfter?.Invoke();
    }

    protected virtual TObject OnCreatePool(string type) =>
      Instantiate(Managers.Prefab.Get(type).GetComponent<TObject>(), parent);

    protected virtual void OnGetPool(TObject obj) {
      obj.OnGet();
      obj.gameObject.SetActive(true);
    }

    protected virtual void OnReleasePool(TObject obj)
    {
      obj.OnReleased();
      obj.gameObject.SetActive(false);
    }

    protected virtual void OnDestroyPool(TObject obj) => Destroy(obj.gameObject);
  }
}
