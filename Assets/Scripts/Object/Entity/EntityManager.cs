using Manager;
using Object.Pool;
using UnityEngine.Pool;

namespace Object.Entity
{
  public class EntityManager : PoolManager<EntityManager, Entity>
  {
    protected override Entity OnCreatePool(string type)
    {
      var prefab = Managers.Prefab.Get(type).GetComponent<Entity>();
      var obj = Instantiate(prefab, prefab is UIEntity ? UIEntity.container : Entity.container);
      obj.pool = pools[type] as IObjectPool<PoolManagement>;
      return obj;
    }
  }
}
