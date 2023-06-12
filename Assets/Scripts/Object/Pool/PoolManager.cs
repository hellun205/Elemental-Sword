using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Manager;
using Object.Entity;
using UnityEngine;
using UnityEngine.Pool;

namespace Object.Pool
{
  public abstract class PoolManager<T, TObject> : GameObjectSingleTon<T> 
    where T : PoolManager<T, TObject>
    where TObject : PoolManagement
  {
    public delegate void PoolEventListener(TObject sender);

    public event PoolEventListener onGetBefore;
    public event PoolEventListener onGetAfter;
    public event PoolEventListener onReleaseBefore;
    public event PoolEventListener onReleaseAfter;

    protected Dictionary<string, IObjectPool<TObject>> pools = new Dictionary<string, IObjectPool<TObject>>();

    protected Transform parent;
    
    private Vector2 startPosition;

    public TComponent Get<TComponent>(Vector2 position,[CanBeNull] Action<TComponent> objSet = null) where TComponent : Component
    {
      var type = typeof(TComponent).Name;
      if (!pools.ContainsKey(type))
      {
        pools.Add(type, new ObjectPool<TObject>(() => OnCreatePool(type), OnGetPool, OnReleasePool, OnDestroyPool));
      }

      startPosition = position;
      var obj = pools[type].Get();
      var objT = obj as TComponent;
      objSet?.Invoke(objT);
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
      pools[type].Release(objT);
    }

    protected virtual TObject OnCreatePool(string type)
    {
      var obj = Instantiate(Managers.Prefab.Get(type).GetComponent<TObject>(), parent);
      obj.pool = (IObjectPool<PoolManagement>)pools[type];
      return obj;
    }
    
    protected virtual void OnGetPool(TObject obj) {
      onGetBefore?.Invoke(obj);
      obj.OnGet();
      obj.gameObject.SetActive(true);
      onGetAfter?.Invoke(obj);
    }

    protected virtual void OnReleasePool(TObject obj)
    {
      onReleaseBefore?.Invoke(obj);
      obj.OnReleased();
      obj.gameObject.SetActive(false);
      onReleaseAfter?.Invoke(obj);
    }

    protected virtual void OnDestroyPool(TObject obj) => Destroy(obj.gameObject);

    protected override void Awake()
    {
      base.Awake();
      onGetBefore += a => a.position = startPosition;
    }
  }
}
