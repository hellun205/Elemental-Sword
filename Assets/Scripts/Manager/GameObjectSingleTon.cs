using UnityEngine;

namespace Manager
{
  public abstract class GameObjectSingleTon<T> : MonoBehaviour, ISingleTon<T> where T : GameObjectSingleTon<T>
  {
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
      if (Instance is null)
        Instance = (T) this;
      else
      {
        Debug.LogError($"Singleton {typeof(T).Name} has multiple objects.");
        Destroy(gameObject);
      }

      if (this is IDontDestroy)
        DontDestroyOnLoad(gameObject);
    }
  }
}
