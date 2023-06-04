namespace Manager
{
  public abstract class SingleTon<T> : ISingleTon<T> where T : SingleTon<T>, new()
  {
    private static T _instance;

    public static T Instance
    {
      get => _instance ??= new T();
      protected set => _instance = value;
    }
  }
}