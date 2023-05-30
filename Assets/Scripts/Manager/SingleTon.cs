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
        Destroy(gameObject);
      
      if (this is IDontDestroyable)
        DontDestroyOnLoad(gameObject);
    }
  }
}