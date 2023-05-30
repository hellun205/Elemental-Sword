using UnityEngine;

namespace Manager
{
  public class SingleTon<T> : MonoBehaviour where T : SingleTon<T>
  {
    public static T instance { get; private set; }

    protected virtual void Awake()
    {
      if (instance is null)
        instance = (T)this;
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